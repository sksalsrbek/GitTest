using System.Windows.Controls;

namespace DPT_WPF
{
    /// <summary>
    /// ucLogin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucLogin : UserControl
    {
        public ucLogin()
        {
            InitializeComponent();
        }
        public ucLogin(DefineValue dv)
        {
            this.DataContext = dv;
        }

    }
}
