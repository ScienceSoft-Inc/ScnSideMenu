ScnSideMenu
======================
Xamarin.Forms side menu control (targeted at Android, iOS and Windows Phone)

How to use this control in Xamarin.Forms app
===========================================
Look sample to know how right include control menu in your application.

If you want to use custom gestures for closing SideMenu then need to add initialize renderers for each platform.

In iOS project just use
```cs
Xamarin.Forms.Forms.Init ();
ViewGesturesRenderer.Init();
```
In Android project just use
```cs
Xamarin.Forms.Forms.Init (this, bundle);
ViewGesturesRenderer.Init();
```
In WinPhone project just use
```cs
Xamarin.Forms.Forms.Init ();
ViewGesturesRenderer.Init();
```