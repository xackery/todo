using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace todo
{
    
    public partial class Main : Form
    {
        public const int WS_EX_STYLE = -20;
        public const uint WS_EX_LAYERED = 0x00080000;
        public const uint WS_EX_TRANSPARENT = 0x00000020;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        bool isOverlayMode = false;

        public Main() {
            //MessageBox.Show($"{GetWindowLong(this.Handle, -20)}");
            InitializeComponent();
            Hide();
            SetOverlayMode(true);
        }

        private void SysTray_MouseDoubleClick(object sender, MouseEventArgs e) {
            SetOverlayMode(!isOverlayMode);            
        }

        private void Main_Resize(object sender, EventArgs e) {
        }

        private void SetOverlayMode(bool value) {
            isOverlayMode = value;

            //sysTray.Text = $"TODO Overlay {(isOverlayMode ? "ON" : "OFF")}";
            uint exStyle = GetWindowLong(this.Handle, WS_EX_STYLE);
            if (isOverlayMode) {                
                FormBorderStyle = FormBorderStyle.None;
                Opacity = 0.5f;
                SetWindowLong(Handle, WS_EX_STYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);                
                return;
            }
            FormBorderStyle = FormBorderStyle.Sizable;
            Opacity = 1f;
            SetWindowLong(Handle, WS_EX_STYLE, exStyle & ~(WS_EX_LAYERED | WS_EX_TRANSPARENT));
            
        }

        private void Main_Load(object sender, EventArgs e) {

        }
    }
}
