using System;
using System.Windows;

namespace SignalLines
{

    public partial class MainWindow
    {
        private ConnectionManager _connectionManager;

        public MainWindow()
        {
            InitializeComponent();

            _connectionManager = new ConnectionManager(new WpfDispatcher());
            _connectionManager.MessageReceived += ConnectionManagerOnMessageReceived;
        }

        private void ConnectionManagerOnMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            MessagesReceived.Text += messageReceivedEventArgs.Message;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var message = MessageToSend.Text;
            _connectionManager.SendMessage(message);
        }
    }   


}
