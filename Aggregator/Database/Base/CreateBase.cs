/*
 * Created by SharpDevelop.
 * User: Cartish
 * Date: 27.02.2017
 * Time: 9:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using Aggregator.Data;
using Aggregator.Database.Local;

namespace Aggregator.Database.Base
{
	/// <summary>
	/// Description of CreateBase.
	/// </summary>
	public static class CreateBase
	{
		public static void CreateBaseOleDb()
		{
			/* Создание файла базы данных */
			CreateDatabase createDataBase;
			try{
				createDataBase = new CreateDatabase(DataConfig.localDatabase, DataConstants.TYPE_OLEDB);
			}catch(Exception ex){
				MessageBox.Show(ex.ToString(), "Ошибка:");
				Application.Exit();
			}
			/* Создание таблиц */
			CreateBaseTables.TableUsers();
			CreateBaseTables.TableCounteragents();
			CreateBaseTables.TableNomenclature();
			CreateBaseTables.TableUnits();
			CreateBaseTables.TableHistory();
		}
	}
}
