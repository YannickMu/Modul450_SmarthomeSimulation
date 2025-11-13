using SmartHomeSimulation;

namespace M320_SmartHome {
    public class BadWC : Zimmer, ILueftung {
        bool LueftungAn { get; set; }
        public Wetterdaten Wetter { get; set; }

        public BadWC() : base("BadWC") {
        }

        public void PruefeLueftung()
        {
            LueftungAn = Temperaturvorgabe > Wetter.Aussentemperatur && !Wetter.Regen && !PersonenImZimmer;
        }
}
