﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;

namespace SyncroSim.STSimStockFlow
{
	internal class FlowSpatialMultiplier
	{
		private int m_FlowGroupId;
        private int? m_FlowMultiplierTypeId;
		private int? m_Iteration;
		private int? m_Timestep;
		private string m_Filename;

		public FlowSpatialMultiplier(
            int flowGroupId, 
            int? flowMultiplierTypeId, 
            int? iteration, 
            int? timestep, 
            string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentException("The filename parameter cannot be Null.");
			}

			this.m_FlowGroupId = flowGroupId;
            this.m_FlowMultiplierTypeId = flowMultiplierTypeId;
			this.m_Iteration = iteration;
			this.m_Timestep = timestep;
			this.m_Filename = fileName;
		}

		public int FlowGroupId
		{
			get
			{
				return this.m_FlowGroupId;
			}
		}

        public int? FlowMultiplierTypeId
        {
            get
            {
                return this.m_FlowMultiplierTypeId;
            }
        }

		public int? Iteration
		{
			get
			{
				return this.m_Iteration;
			}
		}

		public int? Timestep
		{
			get
			{
				return this.m_Timestep;
			}
		}

		public string FileName
		{
			get
			{
				return this.m_Filename;
			}
		}
	}
}