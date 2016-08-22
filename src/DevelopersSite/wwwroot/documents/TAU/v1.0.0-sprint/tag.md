## Tags

Managing tags. Available options:
- Get Tags,
- Add New Tag,
- Modify Tag,
- Remove Tag.

> Tip: for e-commerce usage organize your tags into hierarchy and use it as a category tree.

### Get Tags

Check your available tags. Use built-in filter on your columns.

*Available columns:*

Name    |   Description
---     |   ---
Name    |   Tag Name
Path    |   Hierarchy full path of the given tag
Level   |   Tag level in a given hierarchy path
IsLeaf  |   True / False. True when a tag is the last available (Leaf) node in a path.
DocumentCount   |   Available, current document number relates to the tag.
WordCount   |   Available, current word number relates to the tag.
Id  |   Id of the tag.
ParentTagId |   Parent Tag Id of the tag.

> Tip: use built-in filter in the header to filter your required tags.<br/>
Tip: to copy tag id list select the required tags and press ctrl+c. You can find your JSON Tag ID list in your clipboard.

*Example Tag Window:*

![Demo Image2](img/get_tags.png)

### Add Tag

To add a new tag, you need to fill a sample json document.

Available parameters: *id, name, parentid*.

![Demo Image2](img/add_new_tag.png)

### Modify Tag

To modify a tag, change the tag json document.

**Important** Modifying a tag id, does not influence your tags stored in documents.

![Demo Image2](img/modify_tag.png)

### Remove Tag

Removing a tag, accept your validation form. Tag won't be removed from documents.

**Important** All the children tags will be removed.

![Demo Image2](img/remove_tag.png)