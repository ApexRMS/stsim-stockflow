﻿//*********************************************************************************************
// STSimStockFlow: A SyncroSim Module for the ST-Sim Stocks and Flows Add-In.
//
// Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
//
//*********************************************************************************************

using System;

namespace SyncroSim.STSimStockFlow
{
	internal class FlowSpatialMultiplier
	{
		private int m_FlowGroupId;
		private int? m_Iteration;
		private int? m_Timestep;
		private string m_Filename;

		public FlowSpatialMultiplier(int flowGroupId, int? iteration, int? timestep, string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentException("The filename parameter cannot be Null.");
			}

			this.m_FlowGroupId = flowGroupId;
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