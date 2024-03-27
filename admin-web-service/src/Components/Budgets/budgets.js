import React, { useState, useEffect } from "react";
import { Table, Select } from "antd";
import { useNavigate } from "react-router-dom";

import "./budgets.css"

const Budgets = ({ user }) => {

    const [budgetsData, setBudgetsData] = useState([]);
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
            fetchUsersBudgetsStatistics();
        }

    }, [user]);

    /// <summary>
    ///    Получение статистики бюджетов пользователей
    /// </summary> 
    const fetchUsersBudgetsStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/usersbudgets`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedUsersBudgetsStatistics = data.map((userBudgetsStatistics) => {

                    return {
                        key: userBudgetsStatistics.base,
                        value: userBudgetsStatistics.value 
                    }
                });

                setBudgetsData(receivedUsersBudgetsStatistics);
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
    ///    Получение статистики бюджетов по временным периодов
    /// </summary> 
    const fetchBudgetsByTimePeriodsStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/budgetsbytimeperiods`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedBudgetsByTimePeriodsStatistics = data.map((budgetsByTimePeriodsStatistics) => {

                    return {
                        key: budgetsByTimePeriodsStatistics.base,
                        value: budgetsByTimePeriodsStatistics.value 
                    }
                });

                setBudgetsData(receivedBudgetsByTimePeriodsStatistics);
                setStatisticsBaseTitle("Временной период")
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
                fetchUsersBudgetsStatistics()
                break

            case "budgetsByTimePeriodsStatistics":
                fetchBudgetsByTimePeriodsStatistics()
                break

            default:
                break
        }
    };

    const columns = [
        { title: statisticsBaseTitle, dataIndex: "key"},
        { title: "Число бюджеов", dataIndex: "value"},
    ];

    return (
        <>
            <Select
                defaultValue="usersStatistics"
                style={{ margin: 16 }}
                onChange={handleChange}
                options={[
                    { value: 'usersStatistics', label: 'Статистика по пользователям' },
                    { value: 'budgetsByTimePeriodsStatistics', label: 'Статистика по временным периодам' },
                ]}
            />
            <Table dataSource={budgetsData} columns={columns}/>
        </>
    )
};

export default Budgets;