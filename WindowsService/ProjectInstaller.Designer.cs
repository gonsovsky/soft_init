namespace MyFirstService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WindowsServiceDemo = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // WindowsServiceDemo
            // 
            this.WindowsServiceDemo.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.WindowsServiceDemo.Password = null;
            this.WindowsServiceDemo.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "@WindowsServiceDemo";
            this.serviceInstaller1.DisplayName = "@WindowsServiceDemo";
            this.serviceInstaller1.ServiceName = "@WindowsServiceDemo";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WindowsServiceDemo,
            this.serviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller WindowsServiceDemo;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}