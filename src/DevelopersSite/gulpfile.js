/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    del = require('del'),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    path = require('path'),
    fs = require('fs'),
    deasync = require('deasync'),
    GitHubApi = require("github"),
    Util = require("github/util"),
    mkdirp = require('mkdirp'),
    runSequence = require('run-sequence'),
    _ = require('lodash'),
    Promise = require('promise'),
    gutil = require("gulp-util");

var paths = {
    webroot: "./wwwroot/"
};

if (gutil.env.type === 'production') {
    paths.webroot = "../../../wwwroot/";
}

gulp.task("default", function () {
    console.log("webroot: " + paths.webroot);
    console.info("Generate HTML files: `gulp generate`");
    console.info("Get GitHub documents & generate: `gulp fetch-generate`");
});

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.documents = paths.webroot + "documents/";


gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("generate", function (cb) {
    runSequence(
        "clean:generate",
        ["swagger", "markdown"],
        cb);
});
gulp.task("fetch-generate", function(cb) {
    runSequence(
        ["fetch", "generate"], 
        cb);
});

gulp.task("swagger", function (cb) {
    var finder = require('findit2')(paths.documents);
    var path = require('path');
    var bootprint = require('bootprint');
    var bootprintOpenApi = require('bootprint-openapi');
    var rename_promise = Promise.denodeify(fs.rename);
    var rmdir_promise = Promise.denodeify(rimraf);     

    var files = [];

    finder.on('file', function (file, stat, linkPath) {
        var match = new RegExp(/\.(json)/g);
        if (match.test(file)) {
            files.push(file);
        }
    }).on('end', function () {
        var promises = [];

        files.forEach(function (file) {
            var dir = path.dirname(file);
            var dirOut = dir + '/out';

            var promise = bootprint.load(bootprintOpenApi)
            .merge({
                handlebars: {
                    partials: './bootprint-openapi/handlebars/partials'
                }
            })
            .build(file, dirOut)
            .generate()
            .then(function(){
                return rename_promise(dirOut + '/index.html', dir + '/swagger.html');
            })
            .then(function(){
                return rename_promise(dirOut + '/main.css', dir + '/main.css');
            })
            .then(function(){
                return rmdir_promise(dirOut);
            });

            promises.push(promise);
        });

        Promise.all(promises).then(function() {
            cb();
        })
    });
});

gulp.task('clean:generate', function (cb) {
    console.log("Clean generated files in `"+paths.documents+"`");
    return del(
        [   paths.documents + "**/**/*.html",
            paths.documents + "**/**/*.css",
            paths.documents + "**/**/*.map"
        ],
        { force: true }
        );
});

gulp.task("markdown", function () {
    var marked = require('gulp-marked');
    var options = {
        gfm: true,
        tables: true
    };

    return gulp.src(paths.documents + "**/**/*.md")
        .pipe(marked(options))
        .pipe(gulp.dest(function(file) {
            return file.base;
        }));
});

gulp.task("fetch", function () {

    var github = new GitHubApi({
        version: "3.0.0",
        debug: false,
        protocol: "https",
        host: "api.github.com", 
        pathPrefix: "",
        timeout: 10000
    });

    github.authenticate({
        type: "oauth",
        token: "84ac4cb6c6e8b1c0119092ac5babe646921c1e44"
    });

    var repo = { 
        user: "slamby", 
        repo: "slamby-developers", 
        path: paths.webroot + "documents/", 
        files: ["src/DevelopersSite/wwwroot/documents"]
    };

    repo.files.forEach(function (file) {
        var baseMsg = { user: repo.user, repo: repo.repo };
        getGitContent(github, baseMsg, file, "master", repo.path, file);    
        });
});

function getTags(github, msg) {
    var tagMsg = Util.extend({ page: 1, per_page: 100 }, msg);
    var lastTagsCount = 0;
    var tagNames = [];
    var getTagsSync = deasync(github.repos.getTags);

    do {
        var result = getTagsSync(tagMsg);
        lastTagsCount = result.length;
        
        result.forEach(function (tagData) {
            tagNames.push(tagData.name); 
        });
             
        tagMsg.page++;
    } while (lastTagsCount == tagMsg.per_page);

    return tagNames;
}

function getGitContent(github, msg, repoPath, ref, output, basePath) {
    var contentMsg = Util.extend({ path: repoPath, ref: ref }, msg);

    github.repos.getContent(contentMsg,
        function (error, data) {
            if (error) {
                console.error(JSON.parse(error.message).message + ': ' + msg.user + '/' + msg.repo + '/' + repoPath + ' ' + ref);
                return;
            }
        
            if (Array.isArray(data)) {
                data.forEach(function (item) {
                    getGitContent(github, msg, item.path, ref, output, basePath); 
                });
                
                return;
            }
        
            try {
                var buffer = new Buffer(data.content, 'base64');
                var outPath = repoPath;
                if (_.startsWith(outPath, basePath)){
                    outPath = _.replace(outPath, basePath, '');
                }
                
                outPath = path.join(output, outPath);
                mkdirp.sync(path.dirname(outPath));
                
                var stream = fs.createWriteStream(outPath);
                stream.write(buffer);
                stream.end();
            } catch (err) {
                console.error('\''+ repoPath+'\' not found in ' + ref);
            }
        }
    );
}