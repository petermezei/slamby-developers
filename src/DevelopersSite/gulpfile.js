/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    gulpsync = require('gulp-sync')(gulp),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    path = require('path'),
    fs = require('fs'),
    deasync = require('deasync'),
    GitHubApi = require("github"),
    Util = require("github/util"),
    mkdirp = require('mkdirp'),
    _ = require('lodash');

var paths = {
    webroot: "./wwwroot/"
};

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

gulp.task("generate", gulpsync.sync(["clean:html", "markdown", "swagger"]));
gulp.task("fetch-generate", gulpsync.sync(["fetch", "generate"]));

gulp.task("swagger", function () {
    var finder = require('findit2')(paths.documents);
    var path = require('path');
    var bootprint = require('bootprint');
    var bootprintOpenApi = require('bootprint-openapi');

    return finder.on('file', function (file, stat, linkPath) {
        var match = new RegExp(/\.(json)/g);
        if (match.test(file)) {
            bootprint.load(bootprintOpenApi)
            .merge({
                handlebars: {
                    partials: './bootprint-openapi/handlebars/partials'
                }
            })
            .build(file, path.dirname(file))
            .generate()
            .done(console.log);
        }
    });
});

gulp.task('clean:html', function (cb) {
    rimraf(paths.documents + "**/**/*.html", cb);
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
    var repos = [
        { 
            user: "slamby", 
            repo: "slamby-api", 
            path: paths.webroot + "documents/API/", 
            files: ["swagger.json"],
            ignoredTags: [],
            ignorePatch: true
        },
        { 
            user: "slamby", 
            repo: "slamby-sdk-net", 
            path: paths.webroot + "documents/SDK.NET/", 
            files: ["README.md"],
            ignoredTags: [
                "v0.9.0"
            ],
            ignorePatch: true
        },
        { 
            user: "slamby", 
            repo: "slamby-tau", 
            path: paths.webroot + "documents/TAU/", 
            files: ["readme.md", "img"],
            ignoredTags: [],
            ignorePatch: true
        }
    ];

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

    repos.forEach(function (repo) {
        var baseMsg = { user: repo.user, repo: repo.repo };
        var tags = getTags(github, baseMsg);
    
        tags.forEach(function (tag) {
            
            if (_.indexOf(repo.ignoredTags, tag) != -1) {
                 return;
            }
            if (repo.ignorePatch &&
                !_.endsWith(tag, ".0")) {
                return;
            }

            var tagPath = path.join(repo.path, tag);
            // make sure directory tree created
            mkdirp.sync(tagPath); 
            
            repo.files.forEach(function (file) {
                getGitContent(github, baseMsg, file, tag, tagPath);    
            });
        });
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

function getGitContent(github, msg, repoPath, ref, output) {
    var contentMsg = Util.extend({ path: repoPath, ref: ref }, msg);

    github.repos.getContent(contentMsg,
        function (error, data) {
            if (error) {
                console.error(JSON.parse(error.message).message + ': ' + msg.user + '/' + msg.repo + '/' + repoPath + ' ' + ref);
                return;
            }
        
            if (Array.isArray(data)) {
                // make sure directory tree created
                mkdirp.sync(path.join(output, repoPath));
                
                data.forEach(function (item) {
                    getGitContent(github, msg, item.path, ref, output); 
                });
                
                return;
            }
        
            try {
                var buffer = new Buffer(data.content, 'base64');
                var stream = fs.createWriteStream(path.join(output, repoPath));
                stream.write(buffer);
                stream.end();
            } catch (err) {
                console.error('\''+ repoPath+'\' not found in ' + ref);
            }
        }
    );
}