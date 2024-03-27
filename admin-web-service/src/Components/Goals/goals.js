import React, { useState, useEffect } from "react";
import { Table } from "antd";
import { useNavigate } from "react-router-dom";

import "./goals.css"

const Goals = ({ user }) => {

    const [goalsData, setGoalsData] = useState([]);
    const [statisticsBaseTitle, setStatisticsBaseTitle] = useState("");
    const navigate = useNavigate();

    ///<summary>
    ///    Проверка данных пользователя
    ///</summary>  
    useEffect(() => {      
        const checkUser = async () => {
            if (typeof user === "undefined" ||
                user.token === null ||
                user.login === null) 
            {
                navigate("/");
            }
        }

        checkUser();
    }, [user])

     
    useEffect(() => {

        if (user.token !== null) 
        {
            fetchUsersGoalsStatistics();
        }

    }, [user]);

    /// <summary>
    ///    Получение статистики целей пользователей
    /// </summary> 
    const fetchUsersGoalsStatistics = async () => {

        try 
        {
            const requestOptions = {
                method: "GET",
                headers: { 'Authorization': "Bearer " + user.token }
            }
            const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/api/statistics/usersgoals`, requestOptions)

            if (response.status === 200)
            {
                const data = await response.json();

                console.log(data);
                
                const receivedUsersGoalsStatistics = data.map((userGoalsStatistics) => {

                    return {
                        key: userGoalsStatistics.base,
                        value: userGoalsStatistics.value
                    }
                });

                setGoalsData(receivedUsersGoalsStatistics);
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

    const columns = [
        { title: statisticsBaseTitle, dataIndex: "key"},
        { title: "Число целей", dataIndex: "value"},
    ];

    return (
        <>
            <Table dataSource={goalsData} columns={columns}/>
        </>
    )
};

export default Goals;