﻿// STSimStockFlow: A SyncroSim Package for the ST-Sim Stocks and Flows Add-In.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSimStockFlow
{
	internal class FlowSpatialMultiplierMap : StockFlowMapBase1<FlowSpatialMultiplier>
	{
		public FlowSpatialMultiplierMap(Scenario scenario, FlowSpatialMultiplierCollection items) : base(scenario)
		{
			foreach (FlowSpatialMultiplier Item in items)
			{
				this.TryAddItem(Item);
			}
		}

		public FlowSpatialMultiplier GetFlowSpatialMultiplier(int flowGroupId, int iteration, int timestep)
		{
			return base.GetItem(flowGroupId, iteration, timestep);
		}

		private void TryAddItem(FlowSpatialMultiplier item)
		{
			try
			{
				this.AddItem(item.FlowGroupId, item.Iteration, item.Timestep, item);
			}
			catch (StockFlowMapDuplicateItemException)
			{
				string template = "A duplicate flow spatial multiplier was detected: More information:" + Environment.NewLine + "Flow Group={0}, Iteration={1}, Timestep={2}";
				ExceptionUtils.ThrowArgumentException(template, this.GetFlowGroupName(item.FlowGroupId), StockFlowMapBase.FormatValue(item.Iteration), StockFlowMapBase.FormatValue(item.Timestep));
			}
		}
	}
}