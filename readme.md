# AvalonDock and Caliburn Close Application Issue

This test application demonstrates an issue with AvalonDock and Caliburn.Micro:

* [Dirster.AvalonDock](https://github.com/Dirkster99/AvalonDock) v4.30.0
* [Caliburn.Micro](https://caliburnmicro.com/) v4.0.136-rc

## Description

Using AvalonDock and Caliburn.Micro, if a displayed item overrides the `Screen.CanCloseAsync` method and at least one item returns false (cannot close), the DockingManager disappears even though the items are still present in the collection.

# Reproduction Steps

1. Start CaliburnDockWpfApp

![Initial Avalon Test App](doc/AvalonDock_App_1_AllDocuments.png)

2. Click the main application close "X" button, and a message box appears for BViewModel

![B asks to close](doc/AvalonDock_App_2_MessageBox.png)

3. Click the "No" button in the message box. The Docking Manager disappears

![No Documents](doc/AvalonDock_App_3_AllDocumentsNotVisible.png)

4. If you click the main application close "X" button again, you are prompted again because B never closed

![Still prompted to close](doc/AvalonDock_App_4_StillPromptsUserOnClose.png)


# Comparison App with TabControl

In this application, I just used a `TabControl` for comparison.

1. Start CaliburnTestWpfApp

![Initial TabControl Test App](doc/TabControl_App_1_AllDocuments.png)

2. Click the main application close "X" button, and a message box appears for BViewModel

![B asks to close](doc/TabControl_App_2_MessageBox.png)

3. Click the "No" button in the message box. Only document "B" remains

![No Documents](doc/TabControl_App_3_OnlyBRemains.png)

