﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Reflection;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSimStockFlow
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal class SummaryFlowReport : ExportTransformer
	{
		protected override void Export(string location, ExportType exportType)
		{
			this.InternalExport(location, exportType, true);
		}

		internal void InternalExport(string location, ExportType exportType, bool showMessage)
		{
			ExportColumnCollection columns = this.CreateColumnCollection();

			if (exportType == ExportType.ExcelFile)
			{
				this.ExcelExport(location, columns, this.CreateReportQuery(false), "Flows");
			}
			else
			{
				columns.Remove("ScenarioName");
                this.CSVExport(location, columns, this.CreateReportQuery(true));

				if (showMessage)
				{
					FormsUtilities.InformationMessageBox("Data saved to '{0}'.", location);
				}
			}
		}

		private ExportColumnCollection CreateColumnCollection()
		{
			ExportColumnCollection c = new ExportColumnCollection();
			DataSheet dsterm = this.Project.GetDataSheet(Constants.DATASHEET_TERMINOLOGY_NAME);
			DataSheet dstermSTSim = this.Project.GetDataSheet(Constants.DATASHEET_STSIM_TERMINOLOGY);
			string FlowUnits = TerminologyUtilities.GetTerminology(dsterm, Constants.STOCK_UNITS_COLUMN_NAME);
			string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);
			string PrimaryStratumLabel = null;
			string SecondaryStratumLabel = null;
			string TertiaryStratumLabel = null;

			TerminologyUtilities.GetStratumLabelStrings(dstermSTSim, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);
			string TotalValue = string.Format(CultureInfo.InvariantCulture, "Total Value ({0})", FlowUnits);

			c.Add(new ExportColumn("ScenarioID", "Scenario ID"));
			c.Add(new ExportColumn("ScenarioName", "Scenario"));
			c.Add(new ExportColumn("Iteration", "Iteration"));
			c.Add(new ExportColumn("Timestep", TimestepLabel));
			c.Add(new ExportColumn("FromStratum", "From " + PrimaryStratumLabel));
			c.Add(new ExportColumn("FromSecondaryStratum", "From " + SecondaryStratumLabel));
			c.Add(new ExportColumn("FromTertiaryStratum", "From " + TertiaryStratumLabel));
			c.Add(new ExportColumn("FromStateClass", "From State Class"));
			c.Add(new ExportColumn("FromStock", "From Stock"));
			c.Add(new ExportColumn("TransitionType", "TransitionType"));
			c.Add(new ExportColumn("ToStratum", "To " + PrimaryStratumLabel));
			c.Add(new ExportColumn("ToStateClass", "To State Class"));
			c.Add(new ExportColumn("ToStock", "To Stock"));
			c.Add(new ExportColumn("FlowGroup", "Flow Group"));
			c.Add(new ExportColumn("EndStratum", "End " + PrimaryStratumLabel));
			c.Add(new ExportColumn("EndSecondaryStratum", "End " + SecondaryStratumLabel));
			c.Add(new ExportColumn("EndTertiaryStratum", "End " + TertiaryStratumLabel));
			c.Add(new ExportColumn("EndStateClass", "End State Class"));
			c.Add(new ExportColumn("EndMinAge", "End Min Age"));

			c.Add(new ExportColumn("Amount", TotalValue));

			c["Amount"].DecimalPlaces = 2;
			c["Amount"].Alignment = ColumnAlignment.Right;

			return c;
		}

		private string CreateReportQuery(bool isCSV)
		{
			string ScenFilter = this.CreateActiveResultScenarioFilter();

            string Query =
                "SELECT " +
                "stsim_stockflow__OutputFlow.ScenarioID, ";

            if (!isCSV)
            {
                Query += "system__Scenario.Name AS ScenarioName, ";
            }

            Query += string.Format(CultureInfo.InvariantCulture,
                "stsim_stockflow__OutputFlow.Iteration,  " +
                "stsim_stockflow__OutputFlow.Timestep,  " +
                "ST1.Name AS FromStratum, " +
                "SS1.Name AS FromSecondaryStratum, " +
                "TS1.Name AS FromTertiaryStratum, " +
                "SC1.Name AS FromStateClass, " +
                "STK1.Name AS FromStock, " +
                "stsim__TransitionType.Name AS TransitionType, " +
                "ST2.Name AS ToStratum, " +
                "SC2.Name AS ToStateClass, " +
                "STK2.Name AS ToStock, " +
                "stsim_stockflow__FlowGroup.Name as FlowGroup, " +
                "ST3.Name AS EndStratum, " +
                "SS2.Name AS EndSecondaryStratum, " +
                "TS2.Name AS EndTertiaryStratum, " +
                "SC3.Name AS EndStateClass, " +
                "stsim_stockflow__OutputFlow.EndMinAge, " +
                "stsim_stockflow__OutputFlow.Amount " +
                "FROM stsim_stockflow__OutputFlow " +
                "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = stsim_stockflow__OutputFlow.ScenarioID " +
                "INNER JOIN stsim__Stratum AS ST1 ON ST1.StratumID = stsim_stockflow__OutputFlow.FromStratumID " +
                "INNER JOIN stsim__Stratum AS ST2 ON ST2.StratumID = stsim_stockflow__OutputFlow.ToStratumID " +
                "INNER JOIN stsim__Stratum AS ST3 ON ST3.StratumID = stsim_stockflow__OutputFlow.EndStratumID " +
                "LEFT JOIN stsim__SecondaryStratum AS SS1 ON SS1.SecondaryStratumID = stsim_stockflow__OutputFlow.FromSecondaryStratumID " +
                "LEFT JOIN stsim__SecondaryStratum AS SS2 ON SS2.SecondaryStratumID = stsim_stockflow__OutputFlow.EndSecondaryStratumID " +
                "LEFT JOIN stsim__TertiaryStratum AS TS1 ON TS1.TertiaryStratumID = stsim_stockflow__OutputFlow.FromTertiaryStratumID " +
                "LEFT JOIN stsim__TertiaryStratum AS TS2 ON TS2.TertiaryStratumID = stsim_stockflow__OutputFlow.EndTertiaryStratumID " +
                "INNER JOIN stsim__StateClass AS SC1 ON SC1.StateClassID = stsim_stockflow__OutputFlow.FromStateClassID " +
                "INNER JOIN stsim__StateClass AS SC2 ON SC2.StateClassID = stsim_stockflow__OutputFlow.ToStateClassID " +
                "INNER JOIN stsim__StateClass AS SC3 ON SC3.StateClassID = stsim_stockflow__OutputFlow.EndStateClassID " +
                "INNER JOIN stsim_stockflow__StockType AS STK1 ON STK1.StockTypeID = stsim_stockflow__OutputFlow.FromStockTypeID " +
                "INNER JOIN stsim_stockflow__StockType AS STK2 ON STK2.StockTypeID = stsim_stockflow__OutputFlow.ToStockTypeID " +
                "INNER JOIN stsim_stockflow__FlowGroup ON stsim_stockflow__FlowGroup.FlowGroupID = stsim_stockflow__OutputFlow.FlowGroupID " +
                "LEFT JOIN stsim__TransitionType ON stsim__TransitionType.TransitionTypeID = stsim_stockflow__OutputFlow.TransitionTypeID " +
                "WHERE stsim_stockflow__OutputFlow.ScenarioID IN ({0})  " +
                "ORDER BY " +
                "stsim_stockflow__OutputFlow.ScenarioID, ", ScenFilter);

            if (!isCSV)
            {
                Query += "system__Scenario.Name, ";
            }
             
            Query +=       
                "stsim_stockflow__OutputFlow.Iteration, " + 
                "stsim_stockflow__OutputFlow.Timestep, " + 
                "ST1.Name, " + 
                "SS1.Name, " + 
                "TS1.Name, " + 
                "SC1.Name, " + 
                "STK1.Name, " + 
                "stsim__TransitionType.Name, " + 
                "ST2.Name, " + 
                "SC2.Name, " +
                "STK2.Name, " + 
                "stsim_stockflow__FlowGroup.Name, " + 
                "ST3.Name, " +
                "SS2.Name, " +
                "TS2.Name, " +
                "SC3.Name, " +
                "stsim_stockflow__OutputFlow.EndMinAge";

            return Query;
		}
	}
}
