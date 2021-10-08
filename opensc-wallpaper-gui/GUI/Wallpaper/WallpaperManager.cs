using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Wallpaper
{

    class WallpaperManager
    {

        #region Singleton
        public static WallpaperManager Instance { get; } = new WallpaperManager();
        private WallpaperManager() { }
        #endregion

        public void Initialize() => MainForm.Instance.Load += mainFormLoaded;

        bool mainFormHasLoaded = false;
        private MdiClient mainFormMdiClient;
        private MdiClientWrapperForScrolling mainFormMdiClientScrollWrapper;

        private void mainFormLoaded(object sender, EventArgs e)
        {
            mainFormHasLoaded = true;
            mainFormMdiClient = getMdiClient();
            backgroundImageRenderer.DstSize = mainFormMdiClient.Size;
            mainFormMdiClient.SizeChanged += mainFormMdiContainerSizeChanged;
            setMdiClientDoubleBuffered(mainFormMdiClient, true);
            mainFormMdiClient.Paint += paintMdiClientBackground;
            mainFormMdiClientScrollWrapper = new MdiClientWrapperForScrolling(mainFormMdiClient);
            mainFormMdiClientScrollWrapper.Scroll += mainFormMdiContainerScroll;
            WindowManager.Instance.ChildWindowsSizePositionChanged += mainFormChildWindowsSizePositionChanged;
            rerenderMdiClientBackground();
        }

        private MdiClient getMdiClient() => MainForm.Instance.Controls.OfType<MdiClient>().FirstOrDefault();
        private void setMdiClientDoubleBuffered(MdiClient mdiClient, bool doubleBuffered) =>
             typeof(MdiClient).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(mdiClient, doubleBuffered, null);

        private void mainFormMdiContainerScroll(object sender, ScrollEventArgs e) => repaintMdiClientBackground();

        private void mainFormMdiContainerSizeChanged(object sender, EventArgs e)
        {
            backgroundImageRenderer.DstSize = mainFormMdiClient.Size;
            rerenderMdiClientBackground();
        }

        private void mainFormChildWindowsSizePositionChanged(Form window) => repaintMdiClientBackground();

        Image backgroundImageOriginal;
        Image backgroundImage;
        ImageRenderer backgroundImageRenderer = new();

        private void rerenderMdiClientBackground()
        {
            if (!mainFormHasLoaded)
                return;
            Image oldBackgroundImage = backgroundImage;
            if (backgroundImageOriginal != null)
            {
                try
                {
                    backgroundImage = backgroundImageRenderer.Render(backgroundImageOriginal);
                }
                catch { }
            }
            else
            {
                backgroundImage = null;
            }
            repaintMdiClientBackground();
            oldBackgroundImage?.Dispose();
        }

        private void paintMdiClientBackground(object sender, PaintEventArgs e)
        {
            using (Brush bgBrush = new SolidBrush(backgroundcolorSetting.Value))
            {
                e.Graphics.FillRectangle(bgBrush, 0, 0, mainFormMdiClient.Width, mainFormMdiClient.Height);
            }
            if (backgroundImage != null)
                e.Graphics.DrawImage(backgroundImage, 0, 0);
        }

        private void repaintMdiClientBackground() => mainFormMdiClient?.Invalidate();

        private SettingValueChangedDelegate settingValueChangedHandlerFactory(Action extraAction = null)
        {
            return (s, ov, nv) =>
            {
                extraAction?.Invoke();
                rerenderMdiClientBackground();
            };
        }

        public void IntializeSettings()
        {
            foreach (ISetting setting in settings)
                SettingsManager.Instance.RegisterSetting(setting);
            subscribeSettingValueChangedHandlers();
            SettingsManager.Instance.SettingsLoaded += readSettingValues;
        }

        private void readSettingValues()
        {
            updateFromBackgroundcolorSetting();
            updateFromImagePathSetting();
            updateFromLayoutSetting();
            updateFromOpacitySetting();
            updateFromMonochromeSetting();
            updateFromImageTintSetting();
        }

        private void subscribeSettingValueChangedHandlers()
        {
            backgroundcolorSetting.ValueChanged += settingValueChangedHandlerFactory(updateFromBackgroundcolorSetting);
            imagePathSetting.ValueChanged += settingValueChangedHandlerFactory(updateFromImagePathSetting);
            layoutSetting.ValueChanged += settingValueChangedHandlerFactory(updateFromLayoutSetting);
            opacitySetting.ValueChanged += settingValueChangedHandlerFactory(updateFromOpacitySetting);
            monochromeSetting.ValueChanged += settingValueChangedHandlerFactory(updateFromMonochromeSetting);
            imageTintSetting.ValueChanged += settingValueChangedHandlerFactory(updateFromImageTintSetting);
        }

        private void updateFromBackgroundcolorSetting() { }
        private void updateFromImagePathSetting()
        {
            Image backgroundImageOriginalOld = backgroundImageOriginal;
            try
            {
                backgroundImageOriginal = Image.FromFile(imagePathSetting.Value);
            }
            catch
            {
                backgroundImageOriginal = null;
            }
            backgroundImageOriginalOld?.Dispose();
        }
        private void updateFromLayoutSetting() => backgroundImageRenderer.Layout = layoutSetting.EnumValue;
        private void updateFromOpacitySetting() => backgroundImageRenderer.Opacity = opacitySetting.Value / 100.0f;
        private void updateFromMonochromeSetting() => backgroundImageRenderer.Monochrome = monochromeSetting.Value;
        private void updateFromImageTintSetting() => backgroundImageRenderer.Tint = imageTintSetting.Value;

        public const string SETTINGS_PREFIX = "wallpaper";
        public const string SETTINGS_CATEGORY = "Wallpaper";

        private ISetting[] settings => new ISetting[]
        {
            backgroundcolorSetting,
            imagePathSetting,
            layoutSetting,
            opacitySetting,
            monochromeSetting,
            imageTintSetting
        };

        private readonly Setting<Color> backgroundcolorSetting = new Setting<Color>(
            $"{SETTINGS_PREFIX}.backgroundcolor",
            SETTINGS_CATEGORY,
            "Background color",
            "Background color of the form container of the main window.",
            SystemColors.Window);

        private readonly OpenFileSetting imagePathSetting = new OpenFileSetting(
            $"{SETTINGS_PREFIX}.imagepath",
            SETTINGS_CATEGORY,
            "Background image path",
            "Background image to show in the form container of the main window.",
            "",
            "Image files (*.bmp, *.jpg, *.png)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*",
            true,
            (op) =>
            {
                FileInfo fi = new FileInfo(op);
                string np = System.AppContext.BaseDirectory + "background" + fi.Extension;
                File.Copy(op, np);
                return np;
            });

        private readonly EnumSetting<ImageLayout> layoutSetting = new EnumSetting<ImageLayout>(
            $"{SETTINGS_PREFIX}.layout",
            SETTINGS_CATEGORY,
            "Background image layout",
            "Layout mode of the background image.",
            ImageLayout.Zoom);

        private readonly IntSetting opacitySetting = new IntSetting(
            $"{SETTINGS_PREFIX}.opacity",
            SETTINGS_CATEGORY,
            "Background opacity",
            "Opacity of the background image. 100 means not transparent, 0 means not visible.",
            100, 0, 100);

        private readonly Setting<bool> monochromeSetting = new Setting<bool>(
            $"{SETTINGS_PREFIX}.monochrome",
            SETTINGS_CATEGORY,
            "Monochrome image",
            "Show the background image in monochrome mode.",
            false);

        private readonly Setting<Color> imageTintSetting = new Setting<Color>(
            $"{SETTINGS_PREFIX}.imagetint",
            SETTINGS_CATEGORY,
            "Background image tint",
            "Color of the background image if shown in monochrome mode. Select white to get grayscale image.",
            Color.White);

    }

}
