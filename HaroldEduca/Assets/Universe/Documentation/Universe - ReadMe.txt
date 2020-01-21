=== Universe ===
by LightStriker Software

This is a very simple package that will change how you see your scene flow and how you work in the editor.

It adds a virtual layer above scenes, allow you to create game-wide managers without any kind of maintenance required.


== Bugs, Issues, Support ==

You can contact us directly and we will answer as quickly as possible.

Email : admin@lightstrikersoftware.com
Website : www.lightstrikersoftware.com

You can also contact us on Unity's forum, user "LightStriker".


== FAQ ==

- Mac? Linux?

We didn't test this on Linux, but we did on Mac. 

- iOS? Android?

It was tested in project for both platform and works fine.

- I found a bug!

Contact us right away! We will fix it ASAP.

- I got idea for other features...

Again, contact us! We will look into adding that feature - if humanly possible - as quickly as we can.

- I don't understand how it works, how to use it or how to implement it!

Contact us, we will gladly help you.


== Version ==

1.0:
- Release

2.0:
For some reason I never actually update this package on the store while over the last two years the version we use internally changed drastically.
We shipped 6 games using this tech. However, the version that was on the Store was flawed and had a few issues that had been fixed a while ago.

We simply didn't update the store package.

I deeply apologize for not updating this asset on the store.

[CHANGES]
- Manager prefabs are created on the code reload, not on the first game play anymore.
- The tools relies on the full name of the 00-Universe scene instead of checking for the "00".
- Namespaced all the classes under the Universe namespace.
- Replaced the ManagerBase class by a IManager interface.
[FIXES] 
- Managers now load properly once deployed.
- The tools now relies on the types instead of object names, which is far more reliable. 
- Added support for Unity 5.3 SceneManager.