
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{

    public WheelCollider[] wheels = new WheelCollider[4]; //�� �ݶ��̴��� �޾ƿ´�.
    public Transform[] tires = new Transform[4]; //������ ���ư��� �� ǥ���ϱ����� �޽��� �޾ƿ´�.
    public float maxF = 50.0f; //�ڵ��������� ������ ��
    public float power = 3000.0f; //�ڵ����� �о��ִ� ��(���������δ� �ʹ� ������..)
    public float rot = 45;
    Rigidbody rb;

    void Start()

    {
        rb = GetComponent<Rigidbody>(); //������ٵ� �޾ƿ´�.
        for (int i = 0; i < 4; i++)
        {
            wheels[i].steerAngle = 0; //���� ������ ���ݶ��̴��� ������ �����Ѵٸ� 90���� ����������.
            wheels[i].ConfigureVehicleSubsteps(5, 12, 13);
        }
        rb.centerOfMass = new Vector3(0, 0, 0); //�����߽��� ����� ���缭 ���������� �����ϵ��� �Ѵ�.
    }



    private void Update()
    {
        UpdateMeshesPostion(); //������ ���ư��°� ���̵��� ��
    }

    void FixedUpdate()
    {
        float a = Input.GetAxis("Vertical");
        rb.AddForce(transform.rotation * new Vector3(a * power, 0, a)); //�ڿ��� �о��ش�.

        for (int i = 0; i < 4; i++)
        {
            wheels[i].motorTorque = maxF * a; //������ ������.
        }

        float steer = rot * Input.GetAxis("Horizontal");
        for (int i = 0; i < 2; i++) //�չ����� ȸ���Ѵ�.
        {
            wheels[i].steerAngle = steer; //���⵵ ������ �ݶ��̴��� �����λ���� + 90�� ������Ѵ�.
        }
    }

    void UpdateMeshesPostion()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheels[i].GetWorldPose(out pos, out quat);
            tires[i].position = pos;
            tires[i].rotation = quat;
        }
    }
}