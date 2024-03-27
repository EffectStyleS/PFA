import React, { useState, useEffect } from "react";
import { Table, Select } from "antd";
import { useNavigate } from "react-router-dom";

import "./incomes.css"

const Incomes = ({ user }) => {

    const [incomesData, setIncomesData] = useState([]);
    const [statisticsBaseTitle, setStatisticsBaseTitle] = useState("");
    const navigate = useNavigate();

    ///<summary>
    ///    Проверка данных пользователя
    ///</summary>  
    useEffect(() => {      
        const checkUser = async () => {
            if (typeof user === "undefined" ||
                user.token === null ||
                user.login === null
            ) 
            {
                navigate("/");
            }
        }

        checkUser();
    }, [user])

     
    useEffect(() => {

        if (user.token !== null) 
        {
            fetchUsersIncomesStatistics();
        }

    }, [user]);

    /// <summary>
    ///    Получение статистики доходов пользователей
    /// </summary> 
    const fetchUsersIncomesStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/usersincomes`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedUsersIncomesStatistics = data.map((userIncomesStatistics) => {

                    return {
                        key: userIncomesStatistics.base,
                        value: userIncomesStatistics.value 
                    }
                });

                setIncomesData(receivedUsersIncomesStatistics);
                setStatisticsBaseTitle("Пользователь")
            } 
            else {
                throw new Error("Ошибка при запросе логов");
            }
        }   
        catch (error) {
            console.log(error);               
        }   
    }

    /// <summary>
    ///    Получение статистики доходов по типам
    /// </summary> 
    const fetchIncomesByTypeStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/incomesbytypes`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedIncomesByTypeStatistics = data.map((incomesByTypeStatistics) => {

                    return {
                        key: incomesByTypeStatistics.base,
                        value: incomesByTypeStatistics.value 
                    }
                });

                setIncomesData(receivedIncomesByTypeStatistics);
                setStatisticsBaseTitle("Тип доходов")
            } 
            else {
                throw new Error("Ошибка при запросе логов");
            }
        }   
        catch (error) {
            console.log(error);               
        }   
    }


    const handleChange = (value) => {
        switch (value)
        {
            case "usersStatistics":
                fetchUsersIncomesStatistics()
                break

            case "incomesByTypeStatistics":
                fetchIncomesByTypeStatistics()
                break

            default:
                break
        }
    };

    const columns = [
        { title: statisticsBaseTitle, dataIndex: "key"},
        { title: "Число доходов", dataIndex: "value"},
    ];

    return (
        <>
            <Select
                defaultValue="usersStatistics"
                style={{ margin: 16 }}
                onChange={handleChange}
                options={[
                    { value: 'usersStatistics', label: 'Статистика по пользователям' },
                    { value: 'incomesByTypeStatistics', label: 'Статистика по типам доходов' },
                ]}
            />
            <Table dataSource={incomesData} columns={columns}/>
        </>
    )
};

export default Incomes;