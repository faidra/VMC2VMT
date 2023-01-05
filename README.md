VMC2VMT
====

*VMC2VMT* is an bridge application between [VMC Protocol](https://protocol.vmc.info/) and [Virtual Motion Tracker](https://gpsnmeajp.github.io/VirtualMotionTrackerDocument/).  
VMC2VMT behaves as VMC Marionette Application, which receives the motion data sent by a VMC Performer Application.  
VMC2VMT behaves as VMT User Application, which sends virtual tracker attitude to VMT Driver.

Setup
====

- [Setup Virtual Motion Tracker](https://gpsnmeajp.github.io/VirtualMotionTrackerDocument/setup/)
- Download VMC2VMT

Usege
====

- Start SteamVR
- Start VMC2VMT
- Start any VMC Performer Application

Settings
====

- You can modify the `settings.json` file in the `VMC2VMT_Data/StreamingAssets` folder to change the virtual trackers' assignment
  - its key is HumanBodyBoneName as string
  - its value is VMT's virtual tracker index as integer

Lisence
====

MIT Licence

Copyright (c) 2023 faidra

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Thirdparty Lisences
====

[uOSC](https://github.com/hecomi/uOSC)

The MIT License (MIT)

Copyright (c) 2017 hecomi

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
