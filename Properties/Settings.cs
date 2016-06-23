// Decompiled with JetBrains decompiler
// Type: sysinfo.Properties.Settings
// Assembly: sysinfo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 63C89430-98EF-4253-8AB4-05DB217F1DA8
// Assembly location: C:\Users\Kautzman\Downloads\sysinfo (22).exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace sysinfo.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }
  }
}
