/*
 * Создано в SharpDevelop.
 * Пользователь: Cartish
 * Дата: 08.04.2017
 * Время: 8:03
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Server
{
	/// <summary>
	/// Description of SearchNomenclatureSqlServer.
	/// </summary>
	
	public class SearchNomenclatureSqlServer
	{
		List<Price> priceList;
		List<Nomenclature> nomenclatureList;
		
		SqlConnection sqlConnection;
		SqlCommand sqlCommand;
		SqlDataReader sqlDataReader;
		
		public SearchNomenclatureSqlServer()
		{
			priceList = new List<Price>();
			nomenclatureList = new List<Nomenclature>();
			sqlConnection = new SqlConnection(DataConfig.serverConnection);
		}
		
		public void setPrices(ListView listViewPrices)
		{
			priceList = new List<Price>();
			int count = listViewPrices.Items.Count;
			Price price;
			for(int i = 0; i < count; i++)
			{
				price.counteragentName = listViewPrices.Items[i].SubItems[1].Text;
				price.priceName = listViewPrices.Items[i].SubItems[2].Text;
				priceList.Add(price);
			}
		}
		
		public List<Nomenclature> getFindNomenclature(String nomenclatureID)
		{
			nomenclatureList = new List<Nomenclature>();
			
			String criteriasSearch = getCriteriasSearch(nomenclatureID);
			
			sqlConnection.Open();
			foreach(Price price in priceList){
				sqlCommand = new SqlCommand("SELECT * FROM " + price.priceName + " " + criteriasSearch, sqlConnection);
				
				sqlDataReader = sqlCommand.ExecuteReader();
				Nomenclature nomenclature;
		        while (sqlDataReader.Read())
		        {
		        	nomenclature = new Nomenclature();
		        	nomenclature.Name = sqlDataReader["name"].ToString();
		        	nomenclature.Code = sqlDataReader["code"].ToString();
		        	nomenclature.Series = sqlDataReader["series"].ToString();
		        	nomenclature.Article = sqlDataReader["article"].ToString();
		        	nomenclature.Manufacturer = sqlDataReader["manufacturer"].ToString();
		        	//nomenclature.Units = oleDbDataReader["units"].ToString();
		        	nomenclature.Remainder = (Double)sqlDataReader["remainder"];
		        	nomenclature.Price = (Double)sqlDataReader["price"];
		        	nomenclature.Discount1 = (Double)sqlDataReader["discount1"];
		        	nomenclature.Discount2 = (Double)sqlDataReader["discount2"];
		        	nomenclature.Discount3 = (Double)sqlDataReader["discount3"];
		        	nomenclature.Discount4 = (Double)sqlDataReader["discount4"];
		        	nomenclature.Term = (DateTime)sqlDataReader["term"];
		        	nomenclature.CounteragentName = price.counteragentName;
		        	nomenclature.CounteragentPrice = price.priceName;
		        	nomenclatureList.Add(nomenclature);
		        }
		        sqlDataReader.Close();
			}

			sqlConnection.Close();
			
			return nomenclatureList;
		}
		
		String getCriteriasSearch(String nomenclatureID)
		{
			sqlConnection.Open();
			Nomenclature templeteNomenclature;
			sqlCommand = new SqlCommand("SELECT * FROM Nomenclature WHERE (id = " + nomenclatureID + ")", sqlConnection);
			sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			templeteNomenclature.Name = sqlDataReader["name"].ToString();
			templeteNomenclature.Code = sqlDataReader["code"].ToString();
			templeteNomenclature.Series = sqlDataReader["series"].ToString();
			templeteNomenclature.Article = sqlDataReader["article"].ToString();
			templeteNomenclature.Manufacturer = sqlDataReader["manufacturer"].ToString();
			templeteNomenclature.Price = (Double)sqlDataReader["price"];
			templeteNomenclature.Units = sqlDataReader["units"].ToString();
			sqlDataReader.Close();
			sqlConnection.Close();
			
			
			// Начало построения запроса
			String stringQuery = "WHERE ";
			
			// По Наименованию - точное совпадение
			String str = "";
			String[] words =  templeteNomenclature.Name.Split();
			int count = words.Length;
			int i = 0;
			for(i = 0; i < count; i++){
				if(i == 0) str += "(";
				if(words[i].Length > 2){
					if( i > 0) str += " AND ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(i == (count-1)) str += ")";
			}
			
			str = str.Replace(".", "").Replace(",", "");
			//stringQuery += str;
			if(str != "") {
				stringQuery += str;
				stringQuery += " OR ";
			}
			
			// По Коду, Серии, Артиклу
			stringQuery += "(code = '" + templeteNomenclature.Code + "' AND code <> '')"+
						" OR (series = '" + templeteNomenclature.Series + "' AND series <> '')"+
						" OR (article = '" + templeteNomenclature.Article + "' AND article <> '')";
			
			// По Наименованию - частичное совпадение
			str = "";
			count = words.Length;
			for(i = 0; i < count; i++){
				if(i == 0) str += "(";
				if(words[i].Length > 3 && checkIgnore(words[i]) == false){
					if( i > 0) str += " OR ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(i == (count-1)) str += ")";
			}
			
			str = str.Replace(".", "").Replace(",", "");	
			if(str != "" && str != "()") stringQuery += " OR " + str;
			
			// Упорядочить
			stringQuery += " ORDER BY price ASC";
			
			return stringQuery;
		}
		
		bool checkIgnore(String str)
		{
			Regex rgx = new Regex(@"[0-9]"); //@"[a-zA-Z0-9]" 
			if(rgx.IsMatch(str)) return true;
			rgx = new Regex(@"[/,№,\\]");
			if(rgx.IsMatch(str)) return true;
			return false;
		}
		
		bool ignoreNumber(String str)
		{
			Regex rgx = new Regex(@"[0-9]");
			return rgx.IsMatch(str);
		}
		
		bool ignoreSymbol(String str)
		{
			Regex rgx = new Regex(@"[/,№,\\]");
			return rgx.IsMatch(str);
		}
		
		/* AUTOMATION ======================================================================= */
		public void autoFindNomenclature(ListView sourceListView)
		{
			String criteriasSearch;
			String nomenclatureID;
			int count = sourceListView.Items.Count;
			bool result = false;
			
			for(int i = 0; i < count; i++) {
				// получаем сформированный запрос по критериям выбранной номенклатуры
				nomenclatureID = sourceListView.Items[i].SubItems[1].Text;
				criteriasSearch = getAutoCriteriasSearch(nomenclatureID);
				
				// обработка прайс листов по выбранной номенклатуре
				foreach(Price price in priceList){
					sqlConnection.Open();
					sqlCommand = new SqlCommand("SELECT * FROM " + price.priceName + " " + criteriasSearch, sqlConnection);
					sqlDataReader = sqlCommand.ExecuteReader();
					result = sqlDataReader.Read();
			        
					if(result){
						sourceListView.Items[i].StateImageIndex = 1;
						sourceListView.Items[i].SubItems[6].Text = sqlDataReader["name"].ToString();
			        	sourceListView.Items[i].SubItems[7].Text = sqlDataReader["price"].ToString();
			        	sourceListView.Items[i].SubItems[8].Text = sqlDataReader["manufacturer"].ToString();
			        	sourceListView.Items[i].SubItems[9].Text = sqlDataReader["remainder"].ToString();
			        	sourceListView.Items[i].SubItems[10].Text = sqlDataReader["term"].ToString();
			        	sourceListView.Items[i].SubItems[11].Text = sqlDataReader["discount1"].ToString();
			        	sourceListView.Items[i].SubItems[12].Text = sqlDataReader["discount2"].ToString();
			        	sourceListView.Items[i].SubItems[13].Text = sqlDataReader["discount3"].ToString();
			        	sourceListView.Items[i].SubItems[14].Text = sqlDataReader["discount4"].ToString();
			        	sourceListView.Items[i].SubItems[15].Text = sqlDataReader["code"].ToString();
			        	sourceListView.Items[i].SubItems[16].Text = sqlDataReader["series"].ToString();
			        	sourceListView.Items[i].SubItems[17].Text = sqlDataReader["article"].ToString();
			        	sourceListView.Items[i].SubItems[18].Text = price.counteragentName;
			        	sourceListView.Items[i].SubItems[19].Text = price.priceName;
					}
			        sqlDataReader.Close();
			        sqlConnection.Close();
				}
				
				DataForms.FClient.messageInStatus("Пожалуйста подождите идет процесс обработки списка номенклатуры " + (i+1).ToString() + "/" + count.ToString());
				DataForms.FClient.Update();
				System.Threading.Thread.Sleep(50);
			}
			MessageBox.Show("Обработка списка номенклатуры - завершена!", "Сообщение");
		}
		
		String getAutoCriteriasSearch(String nomenclatureID)
		{
			// получаем данные выбранной номенклатуры
			sqlConnection.Open();
			Nomenclature templeteNomenclature;
			sqlCommand = new SqlCommand("SELECT * FROM Nomenclature WHERE (id = " + nomenclatureID + ")", sqlConnection);
			sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			templeteNomenclature.Name = sqlDataReader["name"].ToString();
			templeteNomenclature.Code = sqlDataReader["code"].ToString();
			templeteNomenclature.Series = sqlDataReader["series"].ToString();
			templeteNomenclature.Article = sqlDataReader["article"].ToString();
			templeteNomenclature.Manufacturer = sqlDataReader["manufacturer"].ToString();
			templeteNomenclature.Price = (Double)sqlDataReader["price"];
			templeteNomenclature.Units = sqlDataReader["units"].ToString();
			sqlDataReader.Close();
			sqlConnection.Close();
			
			// формируем запрос
			String stringQuery = "WHERE ";
			
			String str = "";
			String[] words =  templeteNomenclature.Name.Split();
			int count = words.Length;
			int i = 0;
			for(i = 0; i < count; i++){
				if(i == 0) str += "(";
				if(words[i].Length > 2 && checkIgnore(words[i]) == false){
					if( i > 0) str += " AND ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(ignoreNumber(words[i])){
					if( i > 0) str += " AND ";
					str += "name LIKE '%" + words[i] + "%'";
				}
				if(i == (count-1)) str += ")";
			}
			
			str = str.Replace(".", "%").Replace(",", "%");
			if(str != "") {
				stringQuery += str;
				stringQuery += " OR ";
			}
			
			stringQuery += "(code = '" + templeteNomenclature.Code + "' AND code <> '')"+
						" OR (series = '" + templeteNomenclature.Series + "' AND series <> '')"+
						" OR (article = '" + templeteNomenclature.Article + "' AND article <> '')";
			stringQuery += " ORDER BY price ASC";
			
			return stringQuery;
		}
		/*====================================================================================*/
		
		public List<Nomenclature> getAllNomenclature()
		{
			nomenclatureList = new List<Nomenclature>();
			
			sqlConnection.Open();
			foreach(Price price in priceList){
				sqlCommand = new SqlCommand("SELECT * FROM " + price.priceName + " ORDER BY name ASC", sqlConnection);
				
				sqlDataReader = sqlCommand.ExecuteReader();
				Nomenclature nomenclature;
		        while (sqlDataReader.Read())
		        {
		        	nomenclature = new Nomenclature();
		        	nomenclature.Name = sqlDataReader["name"].ToString();
		        	nomenclature.Code = sqlDataReader["code"].ToString();
		        	nomenclature.Series = sqlDataReader["series"].ToString();
		        	nomenclature.Article = sqlDataReader["article"].ToString();
		        	nomenclature.Manufacturer = sqlDataReader["manufacturer"].ToString();
		        	//nomenclature.Units = sqlDataReader["units"].ToString();
		        	nomenclature.Remainder = (Double)sqlDataReader["remainder"];
		        	nomenclature.Price = (Double)sqlDataReader["price"];
		        	nomenclature.Discount1 = (Double)sqlDataReader["discount1"];
		        	nomenclature.Discount2 = (Double)sqlDataReader["discount2"];
		        	nomenclature.Discount3 = (Double)sqlDataReader["discount3"];
		        	nomenclature.Discount4 = (Double)sqlDataReader["discount4"];
		        	nomenclature.Term = (DateTime)sqlDataReader["term"];
		        	nomenclature.CounteragentName = price.counteragentName;
		        	nomenclature.CounteragentPrice = price.priceName;
		        	nomenclatureList.Add(nomenclature);
		        }
		        sqlDataReader.Close();
			}

			sqlConnection.Close();
			
			return nomenclatureList;
		}
	}
}
