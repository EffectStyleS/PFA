import { React, useState } from "react"
import { Outlet, Link } from "react-router-dom"
import { Layout as LayoutAntd, Menu } from "antd"
import { MobileOutlined, AuditOutlined, LogoutOutlined } from '@ant-design/icons';

import "./layout.css";

const { Content, Footer, Sider } = LayoutAntd

const Layout = ({ user }) => {

    function getItem(label, key, icon) {
        return {
            key,
            icon,
            label
        };
    }
    
    const items = [
        getItem(<Link to={"/expenses"}>Расходы</Link>, '1', <MobileOutlined />),
        getItem(<Link to={"/incomes"}>Доходы</Link>, '2', <AuditOutlined />),
        getItem(<Link to={"/goals"}>Цели</Link>, '3', <AuditOutlined />),
        getItem(<Link to={"/budgets"}>Бюджеты</Link>, '4', <LogoutOutlined />),
        getItem(<Link to={"/logout"}>Выход</Link>, '5', <AuditOutlined />),
    ]

    const [siderCollapsed, setSiderCollapsed] = useState(false);

    return (
        <LayoutAntd      
            style={{
                minHeight: '100vh',
            }}
        >
            { user.token !== null ? (
                <Sider collapsible collapsed={siderCollapsed} onCollapse={(value) => setSiderCollapsed(value)}>
                    <Menu 
                        theme="dark"
                        defaultSelectedKeys={['1']} 
                        mode="inline" 
                        items={items}
                    />
                </Sider>
            ) : null }
            <LayoutAntd>
                <Content className="site-layout" style={{ padding: "0 50px" }}>
                    <Outlet />
                </Content>
                <Footer style={{ textAlign: "center" }}>
                    © Vasilev Sergey, ISPU 2024
                </Footer>
            </LayoutAntd>
        </LayoutAntd>
    )
}

export default Layout