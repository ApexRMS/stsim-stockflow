﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSimStockFlow
{
	internal class OutputStock
	{
		private int m_StratumId;
		private int? m_SecondaryStratumId;
		private int? m_TertiaryStratumId;
		private int m_StateClassId;
		private int m_StockGroupId;
		private double m_Amount;

		public OutputStock(
            int stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int stateClassId, 
            int stockGroupId, 
            double amount)
		{
			this.m_StratumId = stratumId;
			this.m_SecondaryStratumId = secondaryStratumId;
			this.m_TertiaryStratumId = tertiaryStratumId;
			this.m_StateClassId = stateClassId;
			this.m_StockGroupId = stockGroupId;
			this.m_Amount = amount;
		}

		public int StratumId
		{
			get
			{
				return this.m_StratumId;
			}
		}

		public int? SecondaryStratumId
		{
			get
			{
				return this.m_SecondaryStratumId;
			}
		}

		public int? TertiaryStratumId
		{
			get
			{
				return this.m_TertiaryStratumId;
			}
		}

		public int StateClassId
		{
			get
			{
				return this.m_StateClassId;
			}
		}

		public int StockGroupId
		{
			get
			{
				return this.m_StockGroupId;
			}
		}

		public double Amount
		{
			get
			{
				return this.m_Amount;
			}
			set
			{
				this.m_Amount = value;
			}
		}
	}
}