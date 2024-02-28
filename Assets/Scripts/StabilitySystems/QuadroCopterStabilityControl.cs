using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadroCopterStabilityControl : MonoBehaviour
{
    public float proportionalConstant = 1.0f;
    public float integralConstant = 0.1f;
    public float derivativeConstant = 0.01f;

    private float integralTerm = 0f;
    private float previousYawError = 0f;
    private float previousAltitudeError = 0f;

    private void Update()
    {
        // Управление курсом (yaw)
        float desiredYaw = 0f; // Предположим, что желаемый курс равен 0
        float currentYaw = transform.rotation.eulerAngles.y;
        float yawError = desiredYaw - currentYaw;
        float yawInput = proportionalConstant * yawError + integralConstant * integralTerm +
                         derivativeConstant * (previousYawError - yawError) / Time.deltaTime;

        // Управление высотой (altitude)
        float desiredAltitude = 10f; // Предположим, что желаемая высота равна 10
        float currentAltitude = transform.position.y;
        float altitudeError = desiredAltitude - currentAltitude;
        integralTerm += altitudeError * Time.deltaTime;
        float altitudeInput = proportionalConstant * altitudeError + integralConstant * integralTerm +
                              derivativeConstant * (previousAltitudeError - altitudeError) / Time.deltaTime;

        // Обновление предыдущих ошибок
        previousYawError = yawError;
        previousAltitudeError = altitudeError;
    }
}
