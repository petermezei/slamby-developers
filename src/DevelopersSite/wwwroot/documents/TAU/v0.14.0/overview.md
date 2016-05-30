## Quick Overview

First let's examine TAU structure, logic and main parts:

Part Name   |   Description
--- |   ---
Header  |   Top navigation area, displaying the active window name, dataset-selector menu and drop-down settings.
Left Menu   |   Main menu selector is on the left sidebar, displaying the main menu items. Currently Dataset, Data end Services are available.
Status Bar   |   Orange colored status bar at the bottom. Contains additional information and status window button.

*Example TAU print screen*:

![Demo Image2](img/main.png)

### Header

#### Dataset Selector

Quick dataset selector. Check your available datasets in drop-down menu and select one to work with.

> Using TAU you can select one active dataset to work with. Basically you can set a dataset to work with in Dataset menu, or select a new one in this quick selector.

#### Settings

Drop-down settings menu at the top right corner.

*Available Menu Items:*

Name    |   Description
--- |   ---
Settings    |   Available TAU configuration settings.<br/>*Server Settings:* Slamby Server URL & API Secret,<br/>*BulkImportSize*: bulk size during import process.
Refresh |   Refresh TAU. Shortcut key: `F5`.
Help    |   Open Slamby Developers site http://developers.slamby.com.
About   |   Slamby TAU Version number.

> Tip: use Refresh by pressing F5

*TAU drop-down settings menu*:

![Demo Image2](img/header_dropdown.png)

### Left Menu

Main navigation. Available Menu Items:
- Dataset
- Data
- Services

### Status Bar

Orange colored status bar in the bottom. Quick access to descriptive data. Contains Status Window.

#### Status Window

Displays full server-client communication. You can check each request & response manually.

> `Tip:` Use Status Window to analyze your queries to help integration.

> `Warning:` When status window is open, loaded data can cause memory issue.

![Demo Image2](img/status_window.png)