using MySql.Data.MySqlClient;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class useDB : MonoBehaviour
{
    private string address = "database-1.cggc6nzskt5w.ap-northeast-2.rds.amazonaws.com";
    private string db_id = "admin";
    private string db_pw = "Q7whv1JGL8Su9wUUjbLg";
    private string db_name = "DB_TEST";
    private string strConn = "";
    private string userId;
    private string userNickname;

    public static useDB instance;

    public void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(this); }
        else { Destroy(this); }
    }
    public void setId(string str)
    {
        this.userId = str;
    }

    //id 가져오기
    public string getId()
    {
        return this.userId;
    }

    //db 주소, 아이디, 비밀번호, 사용할 디비 설정 및 연결 url
    private void setDB()
    {
        strConn = string.Format("Server={0};Port=3306;Uid={1};Pwd={2};Database={3};",
                        address, db_id, db_pw, db_name);
    }
    // 로그인 파라메터로 아이디, 비밀번호 순으로 
    // 로그인 성공 True 리턴, 로그인 실패 false 리턴

    /*
    public bool Login(string id, string pw)
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            conn.Open();
            bool temp = false;
            string quary = "SELECT PW FROM TB_USER WHERE ID='" + id + "'";
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            if (rdr.Read())
            {
                // 로그인 성공
                if (rdr["PW"].Equals(pw))
                {
                    Debug.Log("로그인 성공");
                    temp = true;
                }
                //비밀번호 틀림
                else
                {
                    Debug.Log("비밀번호 틀림");
                    temp = false;
                }
            }
            //ID 틀림, 신규 회원 가입처리
            else
            {
                Debug.Log("ID 새로 생성");
                rdr.Close();
                conn.Close();
                conn.Open();

                //회원정보 입력
                quary = "INSERT INTO TB_USER (ID, PW, MONEY) VALUES ('" +
                    id + "'" +
                    ", " + "'" +
                    pw + "'" +
                    ", " +
                    "0);";
                command = new MySqlCommand(quary, conn);
                int k = command.ExecuteNonQuery();

                if (k == 1)
                {

                    //무기정보
                    quary = "INSERT INTO TB_WP (USERID) VALUES ('" +
                   id + "'" +
                   ");";
                    command.CommandText = quary;
                    command.ExecuteNonQuery();
                    //캐릭터정보
                    quary = "INSERT INTO TB_CHAR (USERID) VALUES ('" +
                   id + "'" +
                   ")";
                    command.CommandText = quary;
                    command.ExecuteNonQuery();
                }
                temp = true;
            }

            conn.Close();
            if (temp) { userId = id; }
            Debug.Log(string.Format("id : {0} pass : {1}", userId, pw));
            return temp;
        }
    }
    */

    public bool Login(string id, string pw)
    {

        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            conn.Open();
            bool temp = false;
            string quary = "SELECT CAST(AES_DECRYPT(unhex(PW), 'a') AS CHAR(10000) CHARACTER SET utf8) FROM TB_USER WHERE ID = '" + id + "';";
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            if (rdr.Read())
            {
                // 로그인 성공
                if (rdr[0].Equals(pw))
                {
                    Debug.Log("로그인 성공");
                    temp = true;
                }
                //비밀번호 틀림
                else
                {
                    Debug.Log("비밀번호 틀림");
                    temp = false;
                }
            }
            //ID 틀림, 신규 회원 가입처리
            else
            {

                Debug.Log("신규 회원 가입");
                rdr.Close();
                conn.Close();
                conn.Open();

                //회원정보 입력
                quary = "INSERT INTO TB_USER (ID, PW, MONEY) VALUES ('" +
                    id + "'" +
                    ", " +
                    "hex(aes_encrypt('" + pw + "','a'))" +
                    ", " +
                    "0);";
                command = new MySqlCommand(quary, conn);
                int k = command.ExecuteNonQuery();

                if (k == 1)
                {

                    //무기정보
                    quary = "INSERT INTO TB_WP (USERID, WP1) VALUES ('" +
                   id + "'" +
                   ", 'TRUE');";
                    command.CommandText = quary;
                    command.ExecuteNonQuery();
                    //캐릭터정보
                    quary = "INSERT INTO TB_CHAR (USERID, CHAR1) VALUES ('" +
                   id + "'" +
                   ", 'TRUE')";
                    command.CommandText = quary;
                    command.ExecuteNonQuery();
                }
                temp = true;
            }

            conn.Close();
            DontDestroyOnLoad(gameObject);
            if (temp) this.userId = id;
            return temp;
        }
    }

    public void setMoney(int money)
    {
        int temp = getMoney();
        if (temp + money < 0) money = temp;
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            string quary = "UPDATE TB_USER SET MONEY = MONEY + " + money + " WHERE ID='" + this.userId + "'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(quary, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        Debug.Log("현재 보유중인 돈 : "+ getMoney());
    }
    public int getMoney()
    {
        int temp = 0;
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            string quary = "SELECT MONEY FROM TB_USER WHERE ID='" + this.userId + "'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            rdr.Read();
            if (rdr == null)
            {
                conn.Close();
            }
            else
            {
                string str = rdr[0].ToString();
                temp = int.Parse(str);
            }
            conn.Close();
            return temp;
        }
    }

    // 무기
    // wp1, wp2 ... wp11까지 순서대로 true, false 배열에 저장해서 가져옴
    // 배열 크기 11
    public bool[] getWp()
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            bool[] temp = new bool[11];

            string quary = "SELECT * FROM TB_WP WHERE USERID='" + this.userId + "'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            rdr.Read();
            if (rdr == null)
            {
                conn.Close();
            }
            else
            {
                for (int i = 1; i < rdr.FieldCount; i++)
                {
                    if (rdr[i].Equals("TRUE"))
                    {
                        temp[i - 1] = true;
                    }
                    else
                    {
                        temp[i - 1] = false;
                    }
                }

            }
            conn.Close();
            return temp;
        }
    }

    // 옷
    // char1, char2 ... char5까지 순서대로 true, false 배열에 저장해서 가져옴
    // 배열 크기 5
    public bool[] getChar()
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            bool[] temp = new bool[5];

            string quary = "SELECT * FROM TB_CHAR WHERE USERID='" + this.userId + "'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            rdr.Read();
            if (rdr == null)
            {
                conn.Close();
            }
            else
            {
                for (int i = 1; i < rdr.FieldCount; i++)
                {
                    if (rdr[i].Equals("TRUE"))
                    {
                        temp[i - 1] = true;
                    }
                    else
                    {
                        temp[i - 1] = false;
                    }
                }

            }
            conn.Close();
            return temp;
        }
    }

    //무기 이름 넣고 불값 넣으면 업데이트
    public void updateWp(string wpName,bool boolValue)
    {
        string boolString = boolValue ? "TRUE" : "FALSE";
        setDB(); 
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            string quary = "UPDATE TB_WP SET " + wpName + " = '"+boolString+"' WHERE USERID='" + this.userId + "'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(quary, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }

    public void WpAllFalse()
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            
            for (int i = 1; i < 12; i++)
            {
                string wpName = "WP" + i;
                string quary = "UPDATE TB_WP SET " + wpName + " = 'FALSE' WHERE USERID='" + this.userId + "'";
                conn.Open();
                MySqlCommand command = new MySqlCommand(quary, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    public void CharAllFalse()
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            for (int i = 1; i < 6; i++)
            {
                string charName = "CHAR" + i;
                string quary = "UPDATE TB_CHAR SET " + charName + " = 'FALSE' WHERE USERID='" + this.userId + "'";
                conn.Open();
                MySqlCommand command = new MySqlCommand(quary, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    //캐릭터(옷) 이름 넣고 불값 넣기
    public void updateChar(string charName, bool boolValue)
    {
        string boolString = boolValue ? "TRUE" : "FALSE";
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {
            string quary = "UPDATE TB_CHAR SET " + charName + " = '"+ boolString + "' WHERE USERID='" + this.userId + "'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(quary, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }

    public bool setNickName(string name)
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {

            conn.Open();
            string quary = "SELECT NICKNAME FROM TB_NICKNAME WHERE NICKNAME='" + name + "'";
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            //닉네임 설정 불가
            if (rdr.Read())
            {
                Debug.Log("중복된 닉네임 있음");
                rdr.Close();
                conn.Close();
                return false;
            }
            else
            {
                Debug.Log("닉네임 생성");
                rdr.Close();
                conn.Close();
                conn.Open();
                quary = "INSERT INTO TB_NICKNAME (USERID, NICKNAME) VALUES ('" +
                       this.userId + "'" +
                       ", " +
                       "'" +
                       name +
                       "'" +
                       ")";
                command = new MySqlCommand(quary, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            userNickname = name;
            Debug.Log(string.Format("Nickname : {0} ",name));
            return true;
        }
    }
    public void removeNickName()
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {

            conn.Open();
            string quary = "DELETE FROM TB_NICKNAME WHERE USERID = '" + this.userId + "'";
            MySqlCommand command = new MySqlCommand(quary, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
    public string getNickName()
    {
        return this.userNickname;
    }


    //닉네임 중복체크
    public bool CanUseNickname(string nickname)
    {
        setDB();
        using (MySqlConnection conn = new MySqlConnection(strConn))
        {

            conn.Open();
            string quary = "SELECT NICKNAME FROM TB_NICKNAME WHERE NICKNAME='" + nickname + "'";
            MySqlCommand command = new MySqlCommand(quary, conn);
            MySqlDataReader rdr = command.ExecuteReader();

            //닉네임이 존재하면 false 
            if (rdr.Read())
            {
                rdr.Close();
                conn.Close();
                return false;
            }
            //닉네임 사용 가능하면 true
            else
            {
                rdr.Close();
                conn.Close();
                return true;
            }
        }
    }
}
