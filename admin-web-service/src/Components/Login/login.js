import React, { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Form, Input, Space } from "antd"

import "./login.css";


const Login = ({ user, setUser }) => {

    const [errorMessages, setErrorMessages] = useState([])
    const [errorMessageVisible, setErrorMessageVisible] = useState(false);
    const navigate = useNavigate()
    const [userData, setUserData] = useState([]);

    ///<summary>
    ///    Проверка данных, полученных из запроса login
    ///    и установка пользователя
    ///</summary>
    useEffect(() => 
    {
        if (userData !== null)
        {
            console.log("userData: ", userData);
            if (userData.length !== 0)
            {
                setUser({ login: userData.login, token: userData.token });
            }
        }
    }, [userData])

    ///<summary>
    ///    Проверка данных пользователя
    ///    и перенаправление на страницу расходов
    ///</summary>    
    useEffect(() => 
    {
        console.log("user: ", user);
        if (typeof user !== "undefined" &&
            user.login !== null &&
            user.token !== null)
        {
            navigate("/expenses")
        }
    }, [user])

    ///<summary>
    ///    Запрос на вход пользователя
    ///</summary>    
    const login = async (formValues) => {
        setUserData(null, null);
        setErrorMessages([]);
        setErrorMessageVisible(false); 

        const requestOptions = {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                login: formValues.login,
                password: formValues.password,
            }),
        }
        
        try {
            const response = await fetch(
                `${process.env.REACT_APP_API_ENDPOINT}/api/account/login`,
                requestOptions
            )
            if (response.status === 200) {
                const data = await response.json();
                console.log("Data:", data)
    
                if (typeof data !== "undefined" && typeof data.login !== "undefined") {
                    setUserData(data);
                }
            }
            else {
                throw new Error("Ошибка при входе. Проверьте логин и пароль.");
            }
        }
        catch (error) {
            console.log(error);
            setErrorMessages([error.message]);
            setErrorMessageVisible(true); 
        }
            
    }

    return (
        <>
            <div class="container">
                <div class="wrapper">
                    <div class="left">
                        <h1 class="title">Pfa Admin Service</h1>
                    </div>
                    <div class="right">
                        <div class="login">
                            <div style={{ display: "flex", justifyContent: "center", alignItems: "center", height: "100vh" }}>
                                {typeof user !== "undefined" &&
                                    user.login !== null &&
                                    user.token !== null
                                ? (
                                    <h3>Пользователь {user.login} успешно вошел в систему</h3>
                                ) : (
                                    <div class="login-content">
                                        <Space direction="vertical" size="large">
                                            <div className="login-header">
                                                <p>Вход</p>
                                            </div>
                                            <Form
                                                onFinish={login}
                                                name="basic"
                                                labelCol={{ span: 16 }}
                                                wrapperCol={{ span: 16 }}
                                                style={{ maxWidth: 600 }}
                                                initialValues={{ remember: false }}
                                                autoComplete="off"
                                            >
                                                <Form.Item
                                                    label="Логин/Login"
                                                    name="login"
                                                    labelCol={{ span: 10 }}
                                                    rules={[
                                                        { required: true, message: "Please input your login!" },
                                                    ]}
                                                >
                                                    <Input />
                                                </Form.Item>
                                    
                                                <Form.Item
                                                    label="Пароль/Password"
                                                    name="password"
                                                    labelCol={{ span: 10 }}
                                                    rules={[
                                                        { required: true, message: "Please input your password!" },
                                                    ]}
                                                >
                                                    <Input.Password />
                                                </Form.Item>

                                                {errorMessages.length > 0 && errorMessageVisible && (
                                                    <div className="error-message">
                                                    {errorMessages.map((errorMessage, index) => (
                                                        <p key={index}>{errorMessage}</p>
                                                    ))}
                                                    </div>
                                                )}
                                                
                                                <Form.Item wrapperCol={{ offset: 10, span: 16 }}  labelCol={{ span: 10 }}>
                                                    <Button type="primary" htmlType="submit">
                                                        Войти
                                                    </Button>
                                                </Form.Item>
                                            </Form>
                                        </Space>
                                    </div>
                                )}
                            </div>                    
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Login;