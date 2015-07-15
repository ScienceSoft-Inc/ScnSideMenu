ScnSideMenu
======================
Xamarin.Forms side menu control (targeted at Android, iOS and Windows Phone)

How to use this control in Xamarin.Forms app
===========================================
Look sample to know how right include control menu in your application.

If you want to use custom gestures for closing SideMenu then need add initialize renderers for each platform.

In iOS project just use
```cs
Xamarin.Forms.Forms.Init ();
BoxViewGestureRenderer.Init();
```
In Android project just use
```cs
Xamarin.Forms.Forms.Init (this, bundle);
BoxViewGestureRenderer.Init();
```
In WinPhone project just use
```cs
Xamarin.Forms.Forms.Init ();
BoxViewGestureRenderer.Init();
```