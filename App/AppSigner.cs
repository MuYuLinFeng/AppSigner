using BigIntegerCrypt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class AppSigner : Form
    {
        public AppSigner()
        {
            InitializeComponent();
        }


        private void btnApkPath_Click(object sender, EventArgs e)
        {
            ApkPath = selectFile("");
            txtApkPath.Text = ApkPath;
        }

        private void btnKeystorePath_Click(object sender, EventArgs e)
        {
            KeystorePath = selectFile("");
            txtKeystorePath.Text = KeystorePath;
            decryptFile = decryptKeystore();
        }

        private void btnRSAPath_Click(object sender, EventArgs e)
        {
            RSAPath = selectFile("");
            txtRSAPath.Text = RSAPath;
            PassWord = decryptPassword();
            if(string.IsNullOrEmpty(decryptFile.Trim()))
            {
                decryptFile = decryptKeystore();
            }
            if (!string.IsNullOrEmpty(decryptFile.Trim()) || !string.IsNullOrEmpty(PassWord.Trim()))
            {
                searchAlias();
            }
        }

        private void btnPasswordPath_Click(object sender, EventArgs e)
        {
            PassPath = selectFile("");
            txtKeystorePassword.Text = PassPath;
            PassWord = decryptPassword();
            if (!string.IsNullOrEmpty(decryptFile.Trim()) || !string.IsNullOrEmpty(PassWord.Trim()))
            {
                searchAlias();
            }
        }

        string ApkPath = "";
        string KeystorePath = "";
        string RSAPath = "";
        string PassPath = "";
        string PassWord = "";
        string Alias = "";
        string decryptFile = "";
        private void btnsSingerAok_Click(object sender, EventArgs e)
        {
            btnsSingerAok.Enabled = false;
            if (string.IsNullOrEmpty(ApkPath.Trim()))
            {
                MessageBox.Show("app路径不为空");
                return;
            }
            if (string.IsNullOrEmpty(KeystorePath.Trim()))
            {
                MessageBox.Show("keystore路径不为空");
                return;
            }
            if (string.IsNullOrEmpty(RSAPath.Trim()))
            {
                MessageBox.Show("RSA路径不为空");
                return;
            }
            if (string.IsNullOrEmpty(txtKeystorePassword.Text.Trim()))
            {
                MessageBox.Show("keystore密码路径不为空");
                return;
            }

            decryptFile = decryptKeystore();
            if (!string.IsNullOrEmpty(decryptFile))
            {
                string output = exeSingerApk();
                btnsSingerAok.Enabled = true;
                MessageBox.Show(output);
            }
        }

        private string decryptKeystore()
        {
            if (string.IsNullOrEmpty(KeystorePath.Trim()) || string.IsNullOrEmpty(RSAPath.Trim()))
            {
                return "";
            }
            BigDecoder bigdecoder = new BigDecoder();
            return bigdecoder.Decrypt(RSAPath, KeystorePath);
        }

        private string decryptPassword()
        {
            if (string.IsNullOrEmpty(PassPath.Trim()) || string.IsNullOrEmpty(RSAPath.Trim()))
            {
                return "";
            }
            System.Diagnostics.Debug.WriteLine("PassPath  " + PassPath);
            BigDecoder bigdecoder = new BigDecoder();
            System.Diagnostics.Debug.WriteLine("RSAPath  " + RSAPath);
            System.Diagnostics.Debug.WriteLine("PassPath  " + PassPath);
            string KeyFilePath = bigdecoder.Decrypt(RSAPath, PassPath);
            System.Diagnostics.Debug.WriteLine("KeyFilePath  " + KeyFilePath);
            string decryptKey = File.ReadAllText(KeyFilePath);
           
            return decryptKey;
        }

        private string searchAlias()
        {
            string cmdStr = "keytool -list -v -keystore " + decryptFile + " -storepass " + PassWord;
            System.Diagnostics.Debug.WriteLine(cmdStr);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            System.Diagnostics.Debug.WriteLine("=====1111=======");
            p.Start();//启动程序
            System.Diagnostics.Debug.WriteLine("=====2222=======");
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(cmdStr);
            System.Diagnostics.Debug.WriteLine("=====3333=======");
            p.StandardInput.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("=====4444=======");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            System.Diagnostics.Debug.WriteLine("=====5555=======");
            p.WaitForExit();//等待程序执行完退出进程
            System.Diagnostics.Debug.WriteLine("=====6666=======");
            p.Close();
            System.Diagnostics.Debug.WriteLine("=====7777=======");
            System.Diagnostics.Debug.WriteLine(output);
            return output;
        }

        private String exeSingerApk()
        {
            FileInfo fileInfo = new FileInfo(ApkPath);
            string signdApk = ApkPath.Substring(0, ApkPath.Length - 4) + ".signer.apk";
            System.Diagnostics.Debug.WriteLine(signdApk);
            string cmdStr = "jarsigner -verbose -keystore " + decryptFile + " -keypass " + PassWord + " -storepass " + PassWord + " -signedjar " + ApkPath + " temp.apk " + Alias;
            System.Diagnostics.Debug.WriteLine(cmdStr);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(cmdStr);
            p.StandardInput.AutoFlush = true;

            //StreamWriter myStreamWriter = p.StandardInput;
            //myStreamWriter.WriteLine(Alias);
            //p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            System.Diagnostics.Debug.WriteLine(output);
            return output;
        }

        private String selectFile(String filter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] names = fileDialog.FileNames;

                foreach (string file in names)
                {
                    //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return file;
                }

            }
            return "";
        }

    }
}
