using System;

namespace ChiisanaIroiro.Service {
    public interface IGenerateNumberService {
        String GenerateProcessId(String configJson);
        String GenerateRandomNumber(String configJson);
        String GenerateRandomHexNumber(String configJson);
        String GenerateRandomGuid(String configJson);
    }
}