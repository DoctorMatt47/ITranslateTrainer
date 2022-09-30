import Header from "common/components/AppShell/Header/Header";
import {ChildrenProps} from "common/interfaces/props";
import Main from "./Main/Main";

const AppShell = ({children}: ChildrenProps) => {
  return (
    <>
      <Header/>
      <Main>{children}</Main>
    </>
  );
}

export default AppShell;
