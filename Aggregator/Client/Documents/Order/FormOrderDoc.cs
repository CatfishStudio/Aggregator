/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 09.04.2017
 * Время: 15:00
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;
using Aggregator.Client.Directories;

namespace Aggregator.Client.Documents.Order
{
	/// <summary>
	/// Description of FormOrderDoc.
	/// </summary>
	public partial class FormOrderDoc : Form
	{
		public FormOrderDoc()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public String ID;
		OleDb oleDb;
		SqlServer sqlServer;
		String docNumber;
		int selectTableLine = -1;		// выбранная строка в таблице
		
		String getDocNumber()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				OleDbConnection oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
				try{
					
					OleDbCommand oleDbCommand = new OleDbCommand("SELECT MAX(id) FROM Orders", oleDbConnection);
					oleDbConnection.Open();
					var order_id = oleDbCommand.ExecuteScalar();
					oleDbConnection.Close();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ЗА-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					oleDbConnection.Close();
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				SqlConnection sqlConnection = new SqlConnection(DataConfig.serverConnection);
				try{
					SqlCommand sqlCommand = new SqlCommand("SELECT SCOPE_IDENTITY(Orders)", sqlConnection);
					sqlConnection.Open();
					var order_id = sqlCommand.ExecuteScalar();
					sqlConnection.Close();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ЗА-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					sqlConnection.Close();
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			return null;
		}
		
		void listViewClear()
		{
			while(listViewNomenclature.Items.Count > 0){
				listViewNomenclature.Items[0].Remove();
			}
			selectTableLine = -1;
		}
		
		/* =================================================================================================
		 * РАЗДЕЛ: СОБЫТИЙ
		 * =================================================================================================
		 */	
		void FormOrderDocLoad(object sender, EventArgs e)
		{
	
		}
		void FormOrderDocFormClosed(object sender, FormClosedEventArgs e)
		{
			
		}
		void Button1Click(object sender, EventArgs e)
		{
			if(DataForms.FCounteragents != null) DataForms.FCounteragents.Close();
			if(DataForms.FCounteragents == null) {
				DataForms.FCounteragents = new FormCounteragents();
				DataForms.FCounteragents.MdiParent = DataForms.FClient;
				DataForms.FCounteragents.TextBoxReturnValue = counteragentTextBox;
				DataForms.FCounteragents.TypeReturnValue = "name";
				DataForms.FCounteragents.ShowMenuReturnValue();
				DataForms.FCounteragents.Show();
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			counteragentTextBox.Clear();
			listViewClear();
		}
		void CounteragentTextBoxTextChanged(object sender, EventArgs e)
		{
			listViewClear();
		}
		void ButtonNomenclatureAddClick(object sender, EventArgs e)
		{
			FormOrderNomenclature FOrderNomenclature = new FormOrderNomenclature();
			FOrderNomenclature.MdiParent = DataForms.FClient;
			FOrderNomenclature.ListViewReturnValue = listViewNomenclature;
			FOrderNomenclature.Counteragent = counteragentTextBox.Text;
			FOrderNomenclature.loadNomenclature();
			FOrderNomenclature.Show();
		}
		
		
		
	}
}
