using SmartHomeSimulation;

namespace M320_SmartHome {
    public class Wohnzimmer : Zimmer, ILueftung {
        bool LueftungAn { get; set; }
        public Wetterdaten Wetter { get; set; }

        public Wohnzimmer() : base("Wohnzimmer") {
        }

        public void PruefeLueftung()
        {
            LueftungAn = Temperaturvorgabe > Wetter.Aussentemperatur && !Wetter.Regen && !PersonenImZimmer;
        }
    }
}
