import React, { useState, useEffect } from "react";
import { Table, Select } from "antd";
import { useNavigate } from "react-router-dom";

import "./expenses.css"

const Expenses = ({ user }) => {

    const [expensesData, setExpensesData] = useState([]);
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
            fetchUsersExpensesStatistics();
        }

    }, [user]);

    /// <summary>
    ///    Получение статистики расходов пользователей
    /// </summary> 
    const fetchUsersExpensesStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/usersexpenses`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedUsersExpensesStatistics = data.map((userExpensesStatistics) => {

                    return {
                        key: userExpensesStatistics.base,
                        value: userExpensesStatistics.value 
                    }
                });

                setExpensesData(receivedUsersExpensesStatistics);
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
    ///    Получение статистики расходов по типам
    /// </summary> 
    const fetchExpensesByTypeStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/expensesbytypes`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedExpensesByTypeStatistics = data.map((expensesByTypeStatistics) => {

                    return {
                        key: expensesByTypeStatistics.base,
                        value: expensesByTypeStatistics.value 
                    }
                });

                setExpensesData(receivedExpensesByTypeStatistics);
                setStatisticsBaseTitle("Тип расходов")
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
                fetchUsersExpensesStatistics()
                break

            case "expensesByTypeStatistics":
                fetchExpensesByTypeStatistics()
                break

            default:
                break
        }
    };

    const columns = [
        { title: statisticsBaseTitle, dataIndex: "key"},
        { title: "Число расходов", dataIndex: "value"},
    ];

    return (
        <>
            <Select
                defaultValue="usersStatistics"
                style={{ margin: 16 }}
                onChange={handleChange}
                options={[
                    { value: 'usersStatistics', label: 'Статистика по пользователям' },
                    { value: 'expensesByTypeStatistics', label: 'Статистика по типам расходов' },
                ]}
            />
            <Table dataSource={expensesData} columns={columns}/>
        </>
    )
};

export default Expenses;