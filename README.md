ScnSideMenu
======================
Xamarin.Forms side menu control (targeted at Android, iOS and Windows Phone)

Description
===========================================
The control lets you add sliding menues (left and right) to you application.

Control Structure
===========================================
Bellow you may see control schema:
![Main](Screenshots/Droid/SideMenuSample.png)

Screenshots of teh real app usign sliding menu follow (sources of the at https://github.com/ScienceSoft-Inc/XamarinDiscountsApp):
![Main](Screenshots/Droid/SideMenuRealApp.png)

How to use the control in Xamarin.Forms app
===========================================
In order to show or hide the menu show the properties "IsShowLeftPanel" and "IsShowRightPanel" to TRUE or FALSE or use swipe gestures near screen border.

In order to have gestures active you need to have platform specific renderers initialized.

iOS:
```cs
Xamarin.Forms.Forms.Init ();
ViewGesturesRenderer.Init();
```
Android:
```cs
Xamarin.Forms.Forms.Init (this, bundle);
ViewGesturesRenderer.Init();
```
WinPhone:
```cs
Xamarin.Forms.Forms.Init ();
ViewGesturesRenderer.Init();
```

See sample usage here: https://github.com/ScienceSoft-Inc/ScnSideMenu/tree/master/ScnSideMenu/Sample/SimpleSideMenu
