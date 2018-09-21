﻿//*********************************************************************************************
// STSimStockFlow: A SyncroSim Module for the ST-Sim Stocks and Flows Add-In.
//
// Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
//
//*********************************************************************************************

using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.STSimStockFlow
{
	internal abstract class StockFlowMapBase6<T> : StockFlowMapBase
	{
		private MultiLevelKeyMap6<SortedKeyMap2<T>> m_map = new MultiLevelKeyMap6<SortedKeyMap2<T>>();

		protected StockFlowMapBase6(Scenario scenario) : base(scenario)
		{
		}

		protected void AddItem(int? k1, int? k2, int? k3, int? k4, int? k5, int? k6, int? iteration, int? timestep, T item)
		{
			SortedKeyMap2<T> m = this.m_map.GetItemExact(k1, k2, k3, k4, k5, k6);

			if (m == null)
			{
				m = new SortedKeyMap2<T>(SearchMode.ExactPrev);
				this.m_map.AddItem(k1, k2, k3, k4, k5, k6, m);
			}

			T v = m.GetItemExact(iteration, timestep);

			if (v != null)
			{
				ThrowDuplicateItemException();
			}

			m.AddItem(iteration, timestep, item);
			this.SetHasItems();
		}

		protected T GetItemExact(int? k1, int? k2, int? k3, int? k4, int? k5, int? k6, int? iteration, int? timestep)
		{
			SortedKeyMap2<T> m = this.m_map.GetItemExact(k1, k2, k3, k4, k5, k6);

			if (m == null)
			{
				return default(T);
			}

			return m.GetItemExact(iteration, timestep);
		}

		protected T GetItem(int? k1, int? k2, int? k3, int? k4, int? k5, int? k6, int? iteration, int? timestep)
		{
			if (!this.HasItems)
			{
				return default(T);
			}

			SortedKeyMap2<T> p = this.m_map.GetItem(k1, k2, k3, k4, k5, k6);

			if (p == null)
			{
				return default(T);
			}

			return p.GetItem(iteration, timestep);
		}
	}
}