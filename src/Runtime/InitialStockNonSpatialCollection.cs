﻿// STSimStockFlow: A SyncroSim Package for the ST-Sim Stocks and Flows Add-In.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSimStockFlow
{
	internal class InitialStockNonSpatialCollection : KeyedCollection<int, InitialStockNonSpatial>
	{
		protected override int GetKeyForItem(InitialStockNonSpatial item)
		{
			return item.Id;
		}
	}
}