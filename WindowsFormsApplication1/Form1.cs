using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Configuration;
using System.Text;

namespace WindowsFormsApplication1
{
  public class Form1 : Form
  {
    private IContainer components = (IContainer) null;
    private Label label1;
    private Timer timer1;
    private Point lastClick;
        // no config file - use these 2 line
        private readonly string ProcessX = "Minia.exe";
        private readonly string Ip = "127.0.0.1";

        // use config file - use these 2 line
        //private readonly string ProcessX = ConfigurationManager.AppSettings["ExeName"];
        //private readonly string IP = ConfigurationManager.AppSettings["IpAddr"];

        public Form1() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.label1 = new Label();
      this.timer1 = new Timer(this.components);
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(53, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(74, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Login to game";
      this.label1.Click += new EventHandler(this.Label1_Click);
      this.timer1.Interval = 10000;
      this.timer1.Tick += new EventHandler(this.Timer1_Tick);
      this.BackColor = Color.SeaShell;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(189, 40);
      this.Controls.Add((Control) this.label1);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Form1);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Login";
      this.TransparencyKey = Color.Indigo;
      this.Load += new EventHandler(this.Form1_Load);
      this.Shown += new EventHandler(this.Button1_Click);
      this.MouseDown += new MouseEventHandler(this.Form1_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Form1_MouseMove);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    [DllImport("kernel32.dll")]
    private static extern bool CreateProcess(
      string lpApplicationName,
      string lpCommandLine,
      IntPtr lpProcessAttributes,
      IntPtr lpThreadAttributes,
      bool bInheritHandles,
      uint dwCreationFlags,
      IntPtr lpEnvironment,
      string lpCurrentDirectory,
      ref Form1.STARTUPINFO lpStartupInfo,
      out Form1.PROCESS_INFORMATION lpProcessInformation);

    private void LaunchApp(string Process, string IP)
    {
      Form1.STARTUPINFO lpStartupInfo = new Form1.STARTUPINFO();
            Form1.CreateProcess(Process, IP, IntPtr.Zero, IntPtr.Zero, false, 0U, IntPtr.Zero, (string)null, ref lpStartupInfo, out _);
            Console.ReadLine();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
    }

    private void Button1_Click_1(object sender, EventArgs e)
    {
      Application.Exit();
      this.timer1.Start();
    }

    private void Check_Process()
    {
      if (Process.GetProcessesByName("Minia.exe").Length != 0)  

      //if (Process.GetProcessesByName(ConfigurationManager.AppSettings["ExeName"]).Length != 0)
      {
                _ = (int)MessageBox.Show("Game Already Running");
                Application.Exit();
      }
      else
      {
        // no config file - use these line
        this.LaunchApp(this.ProcessX, this.Ip);

        // use config file - use these line
        //this.LaunchApp(ConfigurationManager.AppSettings["ExeName"], ConfigurationManager.AppSettings["IpAddr"]);

        this.timer1.Start();
      }
    }

    private void Form1_Load(object sender, EventArgs e) => this.Check_Process();

    private void Button2_Click(object sender, EventArgs e)
    {
    }

    private void Form1_MouseDown(object sender, MouseEventArgs e) => this.lastClick = e.Location;

    private void Form1_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.Left += e.X - this.lastClick.X;
      this.Top += e.Y - this.lastClick.Y;
    }

    private void Button5_Click(object sender, EventArgs e) => Application.Exit();

    private void Button6_Click(object sender, EventArgs e)
    {
    }

    private void WebBrowser1_DocumentCompleted(
      object sender,
      WebBrowserDocumentCompletedEventArgs e)
    {
    }

    private void Button4_Click(object sender, EventArgs e)
    {
    }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label1_Click(object sender, EventArgs e)
    {
    }

    public struct PROCESS_INFORMATION
    {
      public IntPtr hProcess;
      public IntPtr hThread;
      public uint dwProcessId;
      public uint dwThreadId;
    }

    public struct STARTUPINFO
    {
      public uint cb;
      public string lpReserved;
      public string lpDesktop;
      public string lpTitle;
      public uint dwX;
      public uint dwY;
      public uint dwXSize;
      public uint dwYSize;
      public uint dwXCountChars;
      public uint dwYCountChars;
      public uint dwFillAttribute;
      public uint dwFlags;
      public short wShowWindow;
      public short cbReserved2;
      public IntPtr lpReserved2;
      public IntPtr hStdInput;
      public IntPtr hStdOutput;
      public IntPtr hStdError;
    }

    public struct SECURITY_ATTRIBUTES
    {
      public int length;
      public IntPtr lpSecurityDescriptor;
      public bool bInheritHandle;
    }
  }
}
