﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Vocaluxe.Base;
using Vocaluxe.Lib.Draw;

namespace Vocaluxe.Menu
{
    interface IMenu
    {
        void Initialize(IConfig Config, ISettings Settings, ITheme Theme, IHelper Helper, ILog Log, IBackgroundMusic BackgroundMusic, IDrawing Draw, IGraphics Graphics);

        void LoadTheme();
        void SaveTheme();
        void ReloadTextures();
        void UnloadTextures();
        void ReloadTheme();

        bool HandleInput(KeyEvent KeyEvent);
        bool HandleMouse(MouseEvent MouseEvent);
        bool HandleInputThemeEditor(KeyEvent KeyEvent);
        bool HandleMouseThemeEditor(MouseEvent MouseEvent);

        bool UpdateGame();
        void ApplyVolume();
        void OnShow();
        void OnShowFinish();
        void OnClose();

        bool Draw();
        SRectF GetScreenArea();

        void NextInteraction();
        void PrevInteraction();

        bool NextElement();
        bool PrevElement();

        void ProcessMouseClick(int x, int y);
        void ProcessMouseMove(int x, int y);
    }

    public interface IConfig
    {
        void SetBackgroundMusicVolume(int NewVolume);
        int GetBackgroundMusicVolume();

        EOffOn GetVideosToBackground();
        EOffOn GetVideoBackgrounds();

        ESongMenu GetSongMenuType();
    }

    public interface ISettings
    {
        int GetRenderW();
        int GetRenderH();
        bool IsTabNavigation();

        float GetZFar();
        float GetZNear();

        EGameState GetGameState();
    }

    public interface ITheme
    {
        string GetThemeScreensPath();
        int GetSkinIndex();
        STexture GetSkinTexture(string TextureName);
        STexture GetSkinVideoTexture(string VideoName);

        void SkinVideoResume(string VideoName);
        void SkinVideoPause(string VideoName);

        SColorF GetColor(string ColorName);
        bool GetColor(string ColorName, int SkinIndex, ref SColorF Color);

        void UnloadSkins();
        void ListSkins();
        void LoadSkins();
        void LoadTheme();
    }

    public interface IHelper
    {
    }

    public interface IBackgroundMusic
    {
        bool IsDisabled();
        bool IsPlaying();
        bool SongHasVideo();
        bool VideoEnabled();
        
        void Next();
        void Previous();
        void Pause();
        void Play();

        void ApplyVolume();

        STexture GetVideoTexture();
    }

    public interface IDrawing
    {
        RectangleF GetTextBounds(CText text);
        
        void DrawTexture(STexture Texture, SRectF Rect);
        void DrawTexture(STexture Texture, SRectF Rect, SColorF Color, SRectF Bounds);
        void DrawTextureReflection(STexture Texture, SRectF Rect, SColorF Color, SRectF Bounds, float ReflectionSpace, float ReflectionHeight);

        void DrawColor(SColorF Color, SRectF Rect);
    }

    public interface IGraphics
    {
        void ReloadTheme();
    }

    public interface ILog
    {
        void LogError(string ErrorText);
    }

    [Flags]
    public enum EModifier
    {
        None,
        Shift,
        Alt,
        Ctrl
    }

    public enum ESender
    {
        Mouse,
        Keyboard,
        WiiMote,
        Gamepad
    }

    public struct KeyEvent
    {
        public ESender Sender;
        public bool ModALT;
        public bool ModSHIFT;
        public bool ModCTRL;
        public bool KeyPressed;
        public bool Handled;
        public Keys Key;
        public Char Unicode;
        public EModifier Mod;

        public KeyEvent(ESender sender, bool alt, bool shift, bool ctrl, bool pressed, char unicode, Keys key)
        {
            Sender = sender;
            ModALT = alt;
            ModSHIFT = shift;
            ModCTRL = ctrl;
            KeyPressed = pressed;
            Unicode = unicode;
            Key = key;
            Handled = false;

            EModifier mALT = EModifier.None;
            EModifier mSHIFT = EModifier.None;
            EModifier mCTRL = EModifier.None;

            if (alt)
                mALT = EModifier.Alt;

            if (shift)
                mSHIFT = EModifier.Shift;

            if (ctrl)
                mCTRL = EModifier.Ctrl;

            if (!alt && !shift && !ctrl)
                Mod = EModifier.None;
            else
                Mod = mALT | mSHIFT | mCTRL;
        }
    }

    public struct MouseEvent
    {
        public ESender Sender;
        public int X;
        public int Y;
        public bool LB;     //left button click
        public bool LD;     //left button double click
        public bool RB;     //right button click
        public bool MB;     //middle button click

        public bool LBH;    //left button hold (when moving)
        public bool RBH;    //right button hold (when moving)
        public bool MBH;    //middle button hold (when moving)

        public bool ModALT;
        public bool ModSHIFT;
        public bool ModCTRL;

        public EModifier Mod;
        public int Wheel;

        public MouseEvent(ESender sender, bool alt, bool shift, bool ctrl, int x, int y, bool lb, bool ld, bool rb, int wheel, bool lbh, bool rbh, bool mb, bool mbh)
        {
            Sender = sender;
            X = x;
            Y = y;
            LB = lb;
            LD = ld;
            RB = rb;
            MB = mb;

            LBH = lbh;
            RBH = rbh;
            MBH = mbh;

            ModALT = alt;
            ModSHIFT = shift;
            ModCTRL = ctrl;

            EModifier mALT = EModifier.None;
            EModifier mSHIFT = EModifier.None;
            EModifier mCTRL = EModifier.None;

            if (alt)
                mALT = EModifier.Alt;

            if (shift)
                mSHIFT = EModifier.Shift;

            if (ctrl)
                mCTRL = EModifier.Ctrl;

            if (!alt && !shift && !ctrl)
                Mod = EModifier.None;
            else
                Mod = mALT | mSHIFT | mCTRL;

            Wheel = wheel;
        }
    }

    public enum EGameState
    {
        Start,
        Normal,
        EditTheme
    }

    enum EAspect
    {
        Crop,
        LetterBox,
        Stretch
    }

    #region Structs
    public struct SColorF
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public SColorF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public SColorF(SColorF Color)
        {
            R = Color.R;
            G = Color.G;
            B = Color.B;
            A = Color.A;
        }
    }

    public struct SRectF
    {
        public float X;
        public float Y;
        public float W;
        public float H;
        public float Z;
        public float Rotation; //0..360°

        public SRectF(float x, float y, float w, float h, float z)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
            Z = z;
            Rotation = 0f;
        }

        public SRectF(SRectF rect)
        {
            X = rect.X;
            Y = rect.Y;
            W = rect.W;
            H = rect.H;
            Z = rect.Z;
            Rotation = 0f;
        }
    }

    public struct SPoint3f
    {
        public float X;
        public float Y;
        public float Z;
    }

    public struct SPoint3
    {
        public int X;
        public int Y;
        public int Z;
    }

    public struct STexture
    {
        public int index;
        public int PBO;
        public int ID;

        public string TexturePath;

        public float width;
        public float height;
        public SRectF rect;

        public float w2;    //power of 2 width
        public float h2;    //power of 2 height
        public float width_ratio;
        public float height_ratio;

        public SColorF color;

        public STexture(int Index)
        {
            index = Index;
            PBO = 0;
            ID = -1;
            TexturePath = String.Empty;

            width = 1f;
            height = 1f;
            rect = new SRectF(0f, 0f, 1f, 1f, 0f);

            w2 = 2f;
            h2 = 2f;
            width_ratio = 0.5f;
            height_ratio = 0.5f;

            color = new SColorF(1f, 1f, 1f, 1f);
        }
    }
    #endregion Structs

    #region EnumsConfig
    public enum ERenderer
    {
#if WIN
        TR_CONFIG_DIRECT3D,
#endif
        TR_CONFIG_OPENGL,
        TR_CONFIG_SOFTWARE
    }

    public enum EAntiAliasingModes
    {
        x0 = 0,
        x2 = 2,
        x4 = 4,
        x8 = 8,
        x16 = 16,
        x32 = 32
    }

    public enum EColorDeep
    {
        Bit8 = 8,
        Bit16 = 16,
        Bit24 = 24,
        Bit32 = 32
    }

    public enum ETextureQuality
    {
        TR_CONFIG_TEXTURE_LOWEST,
        TR_CONFIG_TEXTURE_LOW,
        TR_CONFIG_TEXTURE_MEDIUM,
        TR_CONFIG_TEXTURE_HIGH,
        TR_CONFIG_TEXTURE_HIGHEST
    }

    public enum EOffOn
    {
        TR_CONFIG_OFF,
        TR_CONFIG_ON
    }

    public enum EDebugLevel
    {
        // don't change the order!
        TR_CONFIG_OFF,		    //no debug infos
        TR_CONFIG_ONLY_FPS,
        TR_CONFIG_LEVEL1,
        TR_CONFIG_LEVEL2,
        TR_CONFIG_LEVEL3,
        TR_CONFIG_LEVEL_MAX	    //all debug infos
    }

    public enum EBufferSize
    {
        b0 = 0,
        b512 = 512,
        b1024 = 1024,
        b1536 = 1536,
        b2048 = 2048,
        b2560 = 2560,
        b3072 = 3072,
        b3584 = 3584,
        b4096 = 4096
    }

    public enum EPlaybackLib
    {
        PortAudio,
        OpenAL
    }

    public enum EWebcamLib
    {
        OpenCV,
        AForgeNet
    }

    public enum ERecordLib
    {
#if WIN
        DirectSound,
#endif
        PortAudio
    }

    public enum EVideoDecoder
    {
        FFmpeg
    }

    public enum ESongMenu
    {
        //TR_CONFIG_LIST,		    //a simple list
        //TR_CONFIG_DREIDEL,	    //as in ultrastar deluxe
        TR_CONFIG_TILE_BOARD,	//chessboard like
        //TR_CONFIG_BOOK          //for playlists
    }

    public enum ESongSorting
    {
        TR_CONFIG_NONE,
        //TR_CONFIG_RANDOM,
        TR_CONFIG_FOLDER,
        TR_CONFIG_ARTIST,
        TR_CONFIG_ARTIST_LETTER,
        TR_CONFIG_TITLE_LETTER,
        TR_CONFIG_EDITION,
        TR_CONFIG_GENRE,
        TR_CONFIG_LANGUAGE,
        TR_CONFIG_YEAR,
        TR_CONFIG_DECADE
    }

    public enum ECoverLoading
    {
        TR_CONFIG_COVERLOADING_ONDEMAND,
        TR_CONFIG_COVERLOADING_ATSTART,
        TR_CONFIG_COVERLOADING_DYNAMIC
    }

    public enum EGameDifficulty
    {
        TR_CONFIG_EASY,
        TR_CONFIG_NORMAL,
        TR_CONFIG_HARD
    }

    public enum ETimerMode
    {
        TR_CONFIG_TIMERMODE_CURRENT,
        TR_CONFIG_TIMERMODE_REMAINING,
        TR_CONFIG_TIMERMODE_TOTAL
    }

    public enum ETimerLook
    {
        TR_CONFIG_TIMERLOOK_NORMAL,
        TR_CONFIG_TIMERLOOK_EXPANDED
    }

    public enum EBackgroundMusicSource
    {
        TR_CONFIG_NO_OWN_MUSIC,
        TR_CONFIG_OWN_MUSIC,
        TR_CONFIG_ONLY_OWN_MUSIC
    }

    public enum EPlayerInfo
    {
        TR_CONFIG_PLAYERINFO_BOTH,
        TR_CONFIG_PLAYERINFO_NAME,
        TR_CONFIG_PLAYERINFO_AVATAR,
        TR_CONFIG_PLAYERINFO_OFF
    }

    public enum EFadePlayerInfo
    {
        TR_CONFIG_FADEPLAYERINFO_ALL,
        TR_CONFIG_FADEPLAYERINFO_INFO,
        TR_CONFIG_FADEPLAYERINFO_OFF
    }

    public enum ELyricStyle
    {
        Fill,
        Jump,
        Slide,
        Zoom
    }

    #endregion EnumsConfig

    public enum EAlignment
    {
        Left,
        Center,
        Right
    }

    public enum EHAlignment
    {
        Top,
        Center,
        Bottom
    }

    public enum EStyle
    {
        Normal,
        Italic,
        Bold,
        BoldItalic
    }
}
