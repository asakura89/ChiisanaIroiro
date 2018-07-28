using System;

namespace ChiisanaIroiro.Service {
    public interface IMakeLabelService {
        String MakeLabel(String labelText);
        String MakeRegionLabel(String labelText);
    }
}