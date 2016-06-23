using System.Collections.Generic;

namespace sysinfo
{
  public class HDD
  {
    public Dictionary<int, Smart> Attributes = new Dictionary<int, Smart>()
    {
      {
        0,
        new Smart("Invalid")
      },
      {
        1,
        new Smart("Raw read error rate")
      },
      {
        2,
        new Smart("Throughput performance")
      },
      {
        3,
        new Smart("Spinup time")
      },
      {
        4,
        new Smart("Start/Stop count")
      },
      {
        5,
        new Smart("Reallocated sector count")
      },
      {
        6,
        new Smart("Read channel margin")
      },
      {
        7,
        new Smart("Seek error rate")
      },
      {
        8,
        new Smart("Seek timer performance")
      },
      {
        9,
        new Smart("Power-on hours count")
      },
      {
        10,
        new Smart("Spinup retry count")
      },
      {
        11,
        new Smart("Calibration retry count")
      },
      {
        12,
        new Smart("Power cycle count")
      },
      {
        13,
        new Smart("Soft read error rate")
      },
      {
        184,
        new Smart("End-to-End error")
      },
      {
        190,
        new Smart("Airflow Temperature")
      },
      {
        191,
        new Smart("G-sense error rate")
      },
      {
        192,
        new Smart("Power-off retract count")
      },
      {
        193,
        new Smart("Load/Unload cycle count")
      },
      {
        194,
        new Smart("HDD temperature")
      },
      {
        195,
        new Smart("Hardware ECC recovered")
      },
      {
        196,
        new Smart("Reallocation count")
      },
      {
        197,
        new Smart("Current pending sector count")
      },
      {
        198,
        new Smart("Offline scan uncorrectable count")
      },
      {
        199,
        new Smart("UDMA CRC error rate")
      },
      {
        200,
        new Smart("Write error rate")
      },
      {
        201,
        new Smart("Soft read error rate")
      },
      {
        202,
        new Smart("Data Address Mark errors")
      },
      {
        203,
        new Smart("Run out cancel")
      },
      {
        204,
        new Smart("Soft ECC correction")
      },
      {
        205,
        new Smart("Thermal asperity rate (TAR)")
      },
      {
        206,
        new Smart("Flying height")
      },
      {
        207,
        new Smart("Spin high current")
      },
      {
        208,
        new Smart("Spin buzz")
      },
      {
        209,
        new Smart("Offline seek performance")
      },
      {
        220,
        new Smart("Disk shift")
      },
      {
        221,
        new Smart("G-sense error rate")
      },
      {
        222,
        new Smart("Loaded hours")
      },
      {
        223,
        new Smart("Load/unload retry count")
      },
      {
        224,
        new Smart("Load friction")
      },
      {
        225,
        new Smart("Load/Unload cycle count")
      },
      {
        226,
        new Smart("Load-in time")
      },
      {
        227,
        new Smart("Torque amplification count")
      },
      {
        228,
        new Smart("Power-off retract count")
      },
      {
        230,
        new Smart("GMR head amplitude")
      },
      {
        231,
        new Smart("Temperature")
      },
      {
        240,
        new Smart("Head flying hours")
      },
      {
        250,
        new Smart("Read error retry rate")
      }
    };

    public int Index { get; set; }

    public bool IsOK { get; set; }

    public string Model { get; set; }

    public string Type { get; set; }

    public string Serial { get; set; }
  }
}
