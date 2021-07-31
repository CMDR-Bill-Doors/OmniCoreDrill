﻿using System.ComponentModel;
using Verse;

namespace DoctorVanGogh.OmniCoreDrill
{
    internal class GlobalDrillParameters : IExposable
    {
        private Multiplier _commonality;
        private Multiplier _density;

        private Multiplier _drillWork;

        public GlobalDrillParameters()
        {
            _drillWork = new Multiplier();
            _density = new Multiplier();
            _commonality = new Multiplier();

            _drillWork.PropertyChanged += Component_PropertyChanged;
            _density.PropertyChanged += Component_PropertyChanged;
            _commonality.PropertyChanged += Component_PropertyChanged;
        }

        public Multiplier DrillWork => _drillWork;

        public Multiplier Density => _density;

        public Multiplier Commonality => _commonality;

        public void ExposeData()
        {
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                _drillWork.PropertyChanged -= Component_PropertyChanged;
                _density.PropertyChanged -= Component_PropertyChanged;
                _commonality.PropertyChanged -= Component_PropertyChanged;
            }

            Scribe_Deep.Look(ref _drillWork, "drillWork");
            Scribe_Deep.Look(ref _density, "density");
            Scribe_Deep.Look(ref _commonality, "commonality");

            if (Scribe.mode != LoadSaveMode.LoadingVars)
            {
                return;
            }

            _drillWork.PropertyChanged += Component_PropertyChanged;
            _density.PropertyChanged += Component_PropertyChanged;
            _commonality.PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Multiplier.Value):
                    ThingDefGenerator.UpdateAllGeneratedDefs();
                    break;
            }
        }
    }
}