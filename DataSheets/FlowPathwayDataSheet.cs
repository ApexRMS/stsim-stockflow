﻿// STSimStockFlow: A SyncroSim Module for the ST-Sim Stocks and Flows Add-In.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSimStockFlow
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal class FlowPathwayDataSheet : DataSheet
	{
		protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
		{
			base.OnDataSheetChanged(e);

			string p = e.GetValue("PrimaryStratumLabel", "Stratum");

			this.Columns[Constants.FROM_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", p);
			this.Columns[Constants.TO_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "To {0}", p);
		}

		public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
		{
			base.Validate(proposedRow, transferMethod);

			DataSheet DiagramSheet = this.GetDataSheet(Constants.DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME);
			Dictionary<int, bool> StockTypes = LookupKeyUtilities.CreateRecordLookup(DiagramSheet, Constants.STOCK_TYPE_ID_COLUMN_NAME);

			int FromStockTypeId = Convert.ToInt32(proposedRow[Constants.FROM_STOCK_TYPE_ID_COLUMN_NAME]);

			if (!StockTypes.ContainsKey(FromStockTypeId))
			{
				throw new DataException("The 'From Stock' does not exist for this scenario.");
			}

			int ToStockTypeId = Convert.ToInt32(proposedRow[Constants.TO_STOCK_TYPE_ID_COLUMN_NAME]);

			if (!StockTypes.ContainsKey(ToStockTypeId))
			{
				throw new DataException("The 'To Stock' does not exist for this scenario.");
			}
		}

		public override void Validate(DataTable proposedData, DataTransferMethod transferMethod)
		{
			base.Validate(proposedData, transferMethod);

			DataSheet StockTypeSheet = this.Project.GetDataSheet(Constants.DATASHEET_STOCK_TYPE_NAME);
			DataSheet DiagramSheet = this.GetDataSheet(Constants.DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME);
			Dictionary<int, bool> StockTypes = LookupKeyUtilities.CreateRecordLookup(DiagramSheet, Constants.STOCK_TYPE_ID_COLUMN_NAME);

			foreach (DataRow dr in proposedData.Rows)
			{
				int FromStockTypeId = Convert.ToInt32(dr[Constants.FROM_STOCK_TYPE_ID_COLUMN_NAME]);

				if (!StockTypes.ContainsKey(FromStockTypeId))
				{
					string StockTypeName = Convert.ToString(DataTableUtilities.GetTableValue(StockTypeSheet.GetData(), StockTypeSheet.ValueMember, FromStockTypeId, StockTypeSheet.DisplayMember));
					throw new DataException(string.Format(CultureInfo.InvariantCulture, "Cannot import flow pathways because the 'From Stock' does not exist in this scenario: {0}", StockTypeName));
				}

				int ToStockTypeId = Convert.ToInt32(dr[Constants.TO_STOCK_TYPE_ID_COLUMN_NAME]);

				if (!StockTypes.ContainsKey(ToStockTypeId))
				{
					string StockTypeName = Convert.ToString(DataTableUtilities.GetTableValue(StockTypeSheet.GetData(), StockTypeSheet.ValueMember, ToStockTypeId, StockTypeSheet.DisplayMember));
					throw new DataException(string.Format(CultureInfo.InvariantCulture, "Cannot import flow pathways because the 'To Stock' does not exist in this scenario: {0}", StockTypeName));
				}
			}
		}
	}
}