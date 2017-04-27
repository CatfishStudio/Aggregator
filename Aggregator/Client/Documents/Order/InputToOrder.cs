/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 26.04.2017
 * Время: 18:33
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;
using Aggregator.Database.Server;
using Aggregator.Utilits;

namespace Aggregator.Client.Documents.Order
{
	/// <summary>
	/// Description of InputToOrder.
	/// </summary>
	public class InputToOrder
	{
		struct Price {
			public String counteragentName;		// наименование контрагента
			public String priceName;			// идентификатор прайслиста
		}
	
		struct OrderDoc{
			public DateTime docDate;
			public String docNumber;
			public String docName;
			public String docCounteragent;
			public String docAutor;
			public Double docSum;
			public Double docVat;
			public Double docTotal;
			public String docPurchasePlan;
		}
		
		/*
		struct Nomenclature {
			String Name;				// наименование номенклатуры
			String Code;				// код номенклатуры
			String Series;				// серия номенклатуры
			String Article;				// артикул номенклатуры
			String Manufacturer;		// производитель номенклатуры
			String Units;				// единицы измерения номенклатуры
			Double Remainder;			// остаток номенклатуры
			Double Price;				// цена номенклатуры
			Double Discount1;			// скидка 1 номенклатуры
			Double Discount2;			// скидка 2 номенклатуры
			Double Discount3;			// скидка 3 номенклатуры
			Double Discount4;			// скидка 4 номенклатуры
			DateTime Term;				// срок годности номенклатуры
			String CounteragentName;	// наименование контрагента
			String CounteragentPrice;	// идентификатор прайслиста
		}
		*/
		
		OleDbConnection oleDbConnection;
		OleDbCommand oleDbCommand;
		OleDbDataReader oleDbDataReader;
		OleDb oleDb;
		QueryOleDb oleDbQuery;

		SqlConnection sqlConnection;
		SqlCommand sqlCommand;
		SqlDataReader sqlDataReader;
		SqlServer sqlServer;
		QuerySqlServer sqlQuery;
		
		String docPPNumber;
		List<Price> priceList;
		
		String report;
		
		public InputToOrder(String docNumberPurchasePlan)
		{
			docPPNumber = docNumberPurchasePlan;
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				oleDbConnection = new OleDbConnection();
				oleDbConnection.ConnectionString = DataConfig.oledbConnectLineBegin + DataConfig.localDatabase + DataConfig.oledbConnectLineEnd + DataConfig.oledbConnectPass;
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				sqlConnection = new SqlConnection(DataConfig.serverConnection);
			}
		}
		
		public void Execute()
		{
			OrderDoc orderDoc;
			double sum = 0;
			double amount = 0;
			double price = 0;
			double vat = 0;
			double total = 0;
			
			if(loadPrices() == false){
				MessageBox.Show("Докуммент План закупок №" + docPPNumber + " не содержитт прайсов." + Environment.NewLine + 
				                "Создание Заказов на основании Плана закупок невозможно!", "Сообщение");
				Dispose();
				return;
			}
			
			report = "Процесс создания Заказов - запушен!";
			
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				try{
					/* Обход прайсов */
					foreach(Price pl in priceList)
					{
						sum = 0;
						amount = 0;
						price = 0;
						vat = 0;
						total = 0;
						
						/* Создание основной информации документа заказ */
						orderDoc = new OrderDoc();
						orderDoc.docDate =  DateTime.Now;
						orderDoc.docNumber = createDocNumber();
						orderDoc.docName = "Заказ";
						orderDoc.docCounteragent = pl.counteragentName;
						orderDoc.docAutor = DataConfig.userName;
						orderDoc.docSum = 0;
						orderDoc.docVat = 0;
						orderDoc.docTotal = 0;
						orderDoc.docPurchasePlan = docPPNumber;
						
						oleDb = new OleDb(DataConfig.localDatabase);
						oleDb.oleDbCommandSelect.CommandText = "SELECT " +
							"id, nomenclatureID, nomenclatureName, units, amount, " +
							"name, price, manufacturer, remainder, term, discount1, discount2, discount3, discount4, code, series, article, " +
							"counteragentName, counteragentPricelist, " +
							"docPurchasePlan, docOrder " +
							"FROM OrderNomenclature WHERE (docPurchasePlan = '" + docPPNumber + "' AND counteragentName = '" + pl.counteragentName + "')";
						
						oleDb.oleDbCommandUpdate.CommandText = "UPDATE OrderNomenclature SET " +
							"nomenclatureID = @nomenclatureID, nomenclatureName = @nomenclatureName, units = @units, amount = @amount, " +
							"name = @name, price = @price, manufacturer = @manufacturer, remainder = @remainder, term = @term, " +
							"discount1 = @discount1, discount2 = @discount2, discount3 = @discount3, discount4 = @discount4, " +
							"code = @code, series = @series, article = @article, " +
							"counteragentName = @counteragentName, counteragentPricelist = @counteragentPricelist, " +
							"docPurchasePlan = @docPurchasePlan, docOrder = @docOrder " +
							"WHERE ([id] = @id)";
						oleDb.oleDbCommandUpdate.Parameters.Add("@nomenclatureID", OleDbType.Integer, 10, "nomenclatureID");
						oleDb.oleDbCommandUpdate.Parameters.Add("@nomenclatureName", OleDbType.VarChar, 255, "nomenclatureName");
						oleDb.oleDbCommandUpdate.Parameters.Add("@units", OleDbType.VarChar, 255, "units");
						oleDb.oleDbCommandUpdate.Parameters.Add("@amount", OleDbType.Double, 15, "amount");
						oleDb.oleDbCommandUpdate.Parameters.Add("@name", OleDbType.VarChar, 255, "name");
						oleDb.oleDbCommandUpdate.Parameters.Add("@price", OleDbType.Double, 15, "price");
						oleDb.oleDbCommandUpdate.Parameters.Add("@manufacturer", OleDbType.VarChar, 255, "manufacturer");
						oleDb.oleDbCommandUpdate.Parameters.Add("@remainder", OleDbType.Double, 15, "remainder");
						oleDb.oleDbCommandUpdate.Parameters.Add("@term", OleDbType.Date, 15, "term");
						oleDb.oleDbCommandUpdate.Parameters.Add("@discount1", OleDbType.Double, 15, "discount1");
						oleDb.oleDbCommandUpdate.Parameters.Add("@discount2", OleDbType.Double, 15, "discount2");
						oleDb.oleDbCommandUpdate.Parameters.Add("@discount3", OleDbType.Double, 15, "discount3");
						oleDb.oleDbCommandUpdate.Parameters.Add("@discount4", OleDbType.Double, 15, "discount4");
						oleDb.oleDbCommandUpdate.Parameters.Add("@code", OleDbType.VarChar, 255, "code");
						oleDb.oleDbCommandUpdate.Parameters.Add("@series", OleDbType.VarChar, 255, "series");
						oleDb.oleDbCommandUpdate.Parameters.Add("@article", OleDbType.VarChar, 255, "article");
						oleDb.oleDbCommandUpdate.Parameters.Add("@counteragentName", OleDbType.VarChar, 255, "counteragentName");
						oleDb.oleDbCommandUpdate.Parameters.Add("@counteragentPricelist", OleDbType.VarChar, 255, "counteragentPricelist");
						oleDb.oleDbCommandUpdate.Parameters.Add("@docPurchasePlan", OleDbType.VarChar, 255, "docPurchasePlan");
						oleDb.oleDbCommandUpdate.Parameters.Add("@docOrder", OleDbType.VarChar, 255, "docOrder");
						oleDb.oleDbCommandUpdate.Parameters.Add("@id", OleDbType.Integer, 10, "id");
						
						if(oleDb.ExecuteFill("OrderNomenclature")){
						
							foreach(DataRow row in oleDb.dataSet.Tables["OrderNomenclature"].Rows){
								/* Привязываем к документу */
								row["docOrder"] = orderDoc.docNumber;
								
								/* Вычисления */
								price = (Double)row["price"];
								amount = (Double)row["amount"];
								sum += (price * amount);
							}
							
							/* Итоги вычислений */
							sum = Math.Round(sum, 2);
							vat = sum * DataConstants.ConstFirmVAT / 100;
							vat = Math.Round(vat, 2);
							total = sum + vat;
							total = Math.Round(total, 2);
							
							orderDoc.docSum = sum;
							orderDoc.docVat = vat;
							orderDoc.docTotal = total;
							
							/* Сохранение основных данных документа Заказ */
							oleDbQuery = new QueryOleDb(DataConfig.localDatabase);
							oleDbQuery.SetCommand("INSERT INTO Orders " +
								"(docDate, docNumber, docName, docCounteragent, " +
								"docAutor, docSum, docVat, docTotal, docPurchasePlan) " +
								"VALUES ('" + orderDoc.docDate + "', " +
								"'" + orderDoc.docNumber + "', " +
								"'" + orderDoc.docName + "', " +
								"'" + orderDoc.docCounteragent + "', " +
								"'" + orderDoc.docAutor + "', " +
								"" + Conversion.DoubleToString(orderDoc.docSum) + ", " +
								"" + Conversion.DoubleToString(orderDoc.docVat) + ", " +
								"" + Conversion.DoubleToString(orderDoc.docTotal) + ", " +
								"'" + orderDoc.docPurchasePlan + "')");
							if(oleDbQuery.Execute()){
								report += Environment.NewLine;
								report += "Документ Заказ №" + orderDoc.docNumber + " - создан!";
									
								if(oleDb.ExecuteUpdate("OrderNomenclature")){
									report += Environment.NewLine;
									report += "Документ План закупок №" + docPPNumber + " - обновлён!";
								}else{
									report += Environment.NewLine;
									report += "Документ План закупок №" + docPPNumber + " - ошибка обновления!";
								}
								
							}else{
								report += Environment.NewLine;
								report += "Документ Заказ №" + orderDoc.docNumber + " - ошибка создания!";
							}
							
						}else{
							MessageBox.Show("Не удалось загрузить перечень номенклатуры из документа" + docPPNumber + "" + Environment.NewLine + 
				                "Создание Заказов на основании Плана закупок невозможно!", "Сообщение");
							Dispose();
							return;
						}
					}
					
					DataForms.FClient.updateHistory("Orders");
					
					/* Отчёт о проделанной работе */
					Dispose();
					Utilits.Console.Log("[ВВОД НА ОСНОВАНИИ] Отчёт:" + Environment.NewLine + 
					                    report + Environment.NewLine +
					                    "--------------------------------------------------------------------------", 
					                    false, true);
					MessageBox.Show("Обработка Плана закупок №" + docPPNumber + " завершена!", "Сообщение");
				}catch(Exception ex){
					Dispose();
					Utilits.Console.Log("[ОШИБКА] " + ex.Message, false, true);
				}
				
				
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER

			}
		}
		
		public void Dispose()
		{
			if(oleDbConnection != null){
				if(oleDbDataReader != null) oleDbDataReader.Close();
				oleDbConnection.Close();
				oleDbCommand.Dispose();
				oleDbConnection.Dispose();
			}
			if(sqlConnection != null){
				if(sqlDataReader != null) sqlDataReader.Close();
				sqlConnection.Close();
				sqlCommand.Dispose();
				sqlConnection.Dispose();
			}
			if(oleDb != null) oleDb.Dispose();
			if(oleDbQuery != null) oleDbQuery.Dispose();
			if(sqlServer != null) sqlServer.Dispose();
			if(sqlQuery != null) sqlQuery.Dispose();
		}
		
		/* Создать номер для документа Заказ */
		String createDocNumber()
		{
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL) {
				// OLEDB
				try{
					if(oleDbCommand != null) oleDbCommand.Dispose();
					oleDbCommand = new OleDbCommand("SELECT MAX(id) FROM Orders", oleDbConnection);
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
					Utilits.Console.Log("[ОШИБКА]: " + ex.ToString(), false, true);
				}
			} else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER
				try{
					sqlCommand = new SqlCommand("SELECT SCOPE_IDENTITY(Orders)", sqlConnection);
					var order_id = sqlCommand.ExecuteScalar();
					
					int num;
					if (order_id.ToString() == "") num = 1;
					else num = (int)order_id + 1;
					String idStr = num.ToString();
					String numStr = "ЗА-0000000";
					numStr = numStr.Remove((numStr.Length - idStr.Length));
					numStr += idStr;
					return numStr;
				}catch(Exception ex){
					Utilits.Console.Log("[ОШИБКА]: " + ex.Message.ToString(), false, true);
				}
			}
			return null;
		}
		
		/* Загрузка прайсов из Плана закупок */
		bool loadPrices()
		{
			priceList = new List<Price>();
			Price price;
			
			if(DataConfig.typeConnection == DataConstants.CONNETION_LOCAL){
				// OLEDB
				try{
					if(oleDbCommand != null) oleDbCommand.Dispose();
					oleDbCommand = new OleDbCommand("SELECT counteragentName, counteragentPricelist, " +
			                                "docID FROM PurchasePlanPriceLists WHERE (docID = '" + docPPNumber + 
			                                "')", oleDbConnection);
					oleDbConnection.Open();
					
					oleDbDataReader = oleDbCommand.ExecuteReader();
					while(oleDbDataReader.Read())
					{
						price = new Price();
						price.counteragentName = oleDbDataReader["counteragentName"].ToString();
						price.priceName = oleDbDataReader["counteragentPricelist"].ToString();
						priceList.Add(price);
					}
					oleDbDataReader.Close();
					oleDbConnection.Close();
					
					if(priceList.Count > 0) return true;
					else return false;
					
				}catch(Exception ex){
					Utilits.Console.Log("[ОШИБКА] " + ex.Message.ToString(), false, true);
					return false;
				}
			}else if (DataConfig.typeConnection == DataConstants.CONNETION_SERVER){
				// MSSQL SERVER

			}
			return false;
		}
		
	}
}
