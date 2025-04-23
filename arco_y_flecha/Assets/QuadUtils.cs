
using UnityEngine;

public class QuadUtils :MonoBehaviour
{

    public Vector2 debug;
    // q0 = top-right, q1 = top-left, q2 = bottom-left, q3 = bottom-right
    public Vector2 q0, q1, q2, q3;
    public void Set(Vector2 q0, Vector2 q1, Vector2 q2, Vector2 q3)
    {
        this.q0 = q0;
        this.q1 = q1;
        this.q2 = q2;
        this.q3 = q3;
    }
    // Orden: q0 = top-left, q1 = top-right, q2 = bottom-right, q3 = bottom-left
    Vector2 BilinearInterpolate(Vector2 uv)
    {
        float s = (uv.x + 1f) * 0.5f;
        float t = (uv.y + 1f) * 0.5f;

        Vector2 top = Vector2.Lerp(q0, q1, s);
        Vector2 bottom = Vector2.Lerp(q3, q2, s);
        return Vector2.Lerp(bottom, top, t);
    }




    // Inversa: busca uv que produce targetPos en el cuadrilátero
    public Vector2 FindUVInQuad(Vector2 targetPos, int maxIterations = 10, float tolerance = 0.0001f)
    {
       // print("FindUVInQuad " + targetPos);
        Vector2 uv = Vector2.zero; // comienza en el centro (0,0)

        for (int i = 0; i < maxIterations; i++)
        {
            Vector2 guess = BilinearInterpolate(uv);
            Vector2 error = targetPos - guess;

            if (error.magnitude < tolerance)
                break;

            // Derivadas finitas: estimamos cómo cambia el punto cuando cambiamos u o v
            float delta = 0.001f;

            Vector2 du = (BilinearInterpolate(uv + new Vector2(delta, 0)) - guess) / delta;
            Vector2 dv = (BilinearInterpolate(uv + new Vector2(0, delta)) - guess) / delta;

            // Matriz jacobiana aproximada
            Matrix2x2 J = new Matrix2x2(du.x, dv.x, du.y, dv.y);

            // Invertimos la jacobiana y corregimos uv
            if (J.Invert(out Matrix2x2 invJ))
            {
                Vector2 deltaUV = invJ * error;
                uv += deltaUV;
            }
            else
            {
                print("no se pudo invertir");
                break; // no se pudo invertir
            }
        }
        //print("FindUVInQuad debug" + debug);
        debug = uv;
        return uv;
    }

    // Pequeña clase para manejar matrices 2x2 e invertirlas
    private struct Matrix2x2
    {
        public float m00, m01, m10, m11;

        public Matrix2x2(float m00, float m01, float m10, float m11)
        {
            this.m00 = m00; this.m01 = m01;
            this.m10 = m10; this.m11 = m11;
        }

        public static Vector2 operator *(Matrix2x2 m, Vector2 v)
        {
            return new Vector2(
                m.m00 * v.x + m.m01 * v.y,
                m.m10 * v.x + m.m11 * v.y
            );
        }

        public bool Invert(out Matrix2x2 result)
        {
            float det = m00 * m11 - m01 * m10;
            if (Mathf.Abs(det) < 1e-6f)
            {
                result = default;
                return false;
            }

            float invDet = 1f / det;
            result = new Matrix2x2(
                m11 * invDet, -m01 * invDet,
                -m10 * invDet, m00 * invDet
            );
            return true;
        }
    }
}