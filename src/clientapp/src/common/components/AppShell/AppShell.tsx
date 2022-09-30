import Header from "common/components/AppShell/Header/Header";
import Main from "./Main/Main";
import React from "react";

interface AppShellProps {
  children: React.ReactNode
}

const AppShell = ({children}: AppShellProps) => {
  return (
    <>
      <Header/>
      <Main>{children}</Main>
    </>
  );
}

export default AppShell;
