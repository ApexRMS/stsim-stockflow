﻿//*********************************************************************************************
// STSimStockFlow: A SyncroSim Module for the ST-Sim Stocks and Flows Add-In.
//
// Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
//
//*********************************************************************************************

using SyncroSim.Common;
using System.Collections.Generic;

namespace SyncroSim.STSimStockFlow
{
	internal class FlowPathwayMap
	{
		private bool m_HasRecords;
		private MultiLevelKeyMap7<SortedKeyMap4<List<FlowPathway>>> m_Map = new MultiLevelKeyMap7<SortedKeyMap4<List<FlowPathway>>>();

		public FlowPathwayMap(FlowPathwayCollection pathways)
		{
			foreach (FlowPathway sa in pathways)
			{
				this.AddItem(sa);
			}
		}

		public bool HasRecords
		{
			get
			{
				return this.m_HasRecords;
			}
		}

		public void AddItem(FlowPathway pathway)
		{
			SortedKeyMap4<List<FlowPathway>> m1 = this.m_Map.GetItemExact(
                pathway.FromStratumId, pathway.FromStateClassId, pathway.FromStockTypeId, 
                pathway.ToStratumId, pathway.ToStateClassId, pathway.TransitionGroupId, pathway.FlowTypeId);

			if (m1 == null)
			{
				m1 = new SortedKeyMap4<List<FlowPathway>>(SearchMode.ExactPrev);

				this.m_Map.AddItem(pathway.FromStratumId, pathway.FromStateClassId, pathway.FromStockTypeId, 
                    pathway.ToStratumId, pathway.ToStateClassId, pathway.TransitionGroupId, pathway.FlowTypeId, m1);
			}

			List<FlowPathway> l = m1.GetItemExact(pathway.FromMinimumAge, pathway.ToMinimumAge, pathway.Iteration, pathway.Timestep);

			if (l == null)
			{
				l = new List<FlowPathway>();
				m1.AddItem(pathway.FromMinimumAge, pathway.ToMinimumAge, pathway.Iteration, pathway.Timestep, l);
			}

			l.Add(pathway);
			this.m_HasRecords = true;
		}

		public List<FlowPathway> GetFlowPathwayList(
            int iteration, int timestep, int? fromStratumId, int fromStateClassId, int fromStockTypeId, int fromMinimumAge, int? 
            toStratumId, int? toStateClassId, int transitionGroupId, int flowTypeId, int toMinimumAge)
		{    
			if (!this.m_HasRecords)
			{
				return null;
			}

			SortedKeyMap4<List<FlowPathway>> m1 = this.m_Map.GetItem(
                fromStratumId, fromStateClassId, fromStockTypeId, 
                toStratumId, toStateClassId, transitionGroupId, flowTypeId);

			if (m1 == null)
			{
				return null;
			}

			List<FlowPathway> l = m1.GetItem(fromMinimumAge, toMinimumAge, iteration, timestep);

			if (l == null || l.Count == 0)
			{
				return null;
			}

			return l;
		}
	}
}