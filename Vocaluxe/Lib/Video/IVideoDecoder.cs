﻿#region license
// /*
//     This file is part of Vocaluxe.
// 
//     Vocaluxe is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     Vocaluxe is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with Vocaluxe. If not, see <http://www.gnu.org/licenses/>.
//  */
#endregion

using System;
using VocaluxeLib.Menu;

namespace Vocaluxe.Lib.Video
{
    struct SVideoStreams
    {
        public int Handle;
        public string File;

        public SVideoStreams(int stream)
        {
            Handle = stream;
            File = String.Empty;
        }
    }

    interface IVideoDecoder
    {
        bool Init();
        void CloseAll();

        int Load(string videoFileName);
        bool Close(int streamID);
        int GetNumStreams();

        float GetLength(int streamID);
        bool GetFrame(int streamID, ref STexture frame, float time, out float videoTime);
        bool Skip(int streamID, float start, float gap);
        void SetLoop(int streamID, bool loop);
        void Pause(int streamID);
        void Resume(int streamID);

        bool Finished(int streamID);

        void Update();
    }
}