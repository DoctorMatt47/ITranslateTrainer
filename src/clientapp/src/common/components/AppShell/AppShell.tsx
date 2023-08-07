import Header from "common/components/AppShell/Header/Header";
import Main from "./Main/Main";
import React from "react";

interface AppShellProps {
  children: React.ReactNode
}

export default function AppShell({ children }: AppShellProps) {
  return (
    <>
      <Header />
      <Main>{children}</Main>
    </>
  );
};
