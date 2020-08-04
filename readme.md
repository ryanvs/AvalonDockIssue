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

5. Snoop shows the `PART_SelectedContentHost` is empty and Width is 0.

![Still prompted to close](doc/AvalonDock_App_5_SnoopAfterCloseCancelled.png)

6. If you use the menu to recreate A, B, and C, only A and C are visible in AvalonDock. This is because B was never actually closed and still exists, just not shown in AvalonDock.

![Still prompted to close](doc/AvalonDock_App_6_RecreateNewDocuments.png)
