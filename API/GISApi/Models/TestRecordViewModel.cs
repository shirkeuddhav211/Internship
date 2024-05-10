using System.Collections.Generic;

namespace GISApi.Models
{
    public class TestRecordViewModel
    {
        public int Id { get; set; }
        public string PartNo { get; set; }
        public string NoOfModbus { get; set; }
        public string SerialParallelDual { get; set; }
        public string OrderSerial { get; set; }
        public string FirmwarePartNo { get; set; }
        public string Version { get; set; }
        public string CustomerName { get; set; }

        public string PartType { get; set; }

        public string Username { get; set; }
        public string Build { get; set; }
        public bool BuildCheck { get; set; }
        public string Machine { get; set; }
        public bool MachineCheck { get; set; }
        public string Warning { get; set; }
        public bool WarningCheck { get; set; }
        public string PrimaryValue { get; set; }
        public bool PrimaryCheck { get; set; }
        public string Secondary { get; set; }
        public bool SecondaryCheck { get; set; }
        public string Connection { get; set; }
        public bool ConnectionCheck { get; set; }
        public string Power { get; set; }
        public bool PowerCheck { get; set; }
        public string Voltage { get; set; }
        public string CurrentValue { get; set; }
        public string DownRatedOutput { get; set; }
        public bool DownRatedOutputCheck { get; set; }
        public string Configured { get; set; }
        public bool ConfiguredCheck { get; set; }
        public string Calibrated { get; set; }
        public bool CalibratedCheck { get; set; }
        public string CkeckOfUut { get; set; }
        public bool CkeckOfUutCheck { get; set; }
        public string OptionalEquipments { get; set; }
        public bool OptionalEquipmentsCheck { get; set; }
        public string Contactor { get; set; }
        public bool Contactor1Check { get; set; }
        public bool Contactor2Check { get; set; }
        public bool Contactor3Check { get; set; }
        public string Profibbu { get; set; }
        public bool Profibbu1Check { get; set; }
        public bool Profibbu2Check { get; set; }
        public bool Profibbu3Check { get; set; }
        public string Analogue { get; set; }
        public bool Analogue1Check { get; set; }
        public bool Analogue2Check { get; set; }
        public bool Analogue3Check { get; set; }
        public string ControlBox { get; set; }
        public bool ControlBoxCheck { get; set; }
        public bool ControlKraftBoxCheck { get; set; }
        public bool Externalcheck { get; set; }
        public string PanelReverser { get; set; }
        public bool PanelReverser1Check { get; set; }
      
        public string Configuration { get; set; }
        public string Configuration2 { get; set; }
        public bool ConfigurationCheck { get; set; }
        public string OtherOptions { get; set; }
        public string OtherOptions2 { get; set; }
        public bool OtherOptionsCheck { get; set; }
        public string CurrentShare { get; set; }
        public bool CurrentShareCheck { get; set; }
        public string Linearity { get; set; }
        public bool LinearityCheck { get; set; }
        public string PowerTest { get; set; }
        public bool PowerTestCheck { get; set; }

        public string UnitApprovedDelivery { get; set; }

        public string SubmittedDate { get; set; }
        public int ShuntVoltage { get; set; }

        public List<CurrentControlModel> CurrentControl { get; set; } = new List<CurrentControlModel>();

        public List<VoltageControlModel> VoltageControl { get; set; } = new List<VoltageControlModel>();

    }


    public class CurrentControlModel
    {
        public int CurrentPercentage { get; set; }
        public string ActualVoltage { get; set; }
        public string ActualCurrent { get; set; }
        public string ShuntVoltage { get; set; }
        public string OutputCurrent { get; set; }
        public string RippleVoltage { get; set; }
        public bool Apporved { get; set; }
        public string Remark { get; set; }
    }
   
    public class VoltageControlModel
    {

        public int CurrentPercentage { get; set; }
        public string ActualVoltage { get; set; }
        public string OutputVoltage { get; set; }
        public string ActualCurrent { get; set; }
        public string RippleVoltage { get; set; }
        public bool Apporved { get; set; }
        public string Remark { get; set; }
    }
}


