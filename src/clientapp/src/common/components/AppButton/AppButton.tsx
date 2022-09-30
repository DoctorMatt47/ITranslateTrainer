import React from "react";
import "./AppButton.scss";

interface AppButtonProps extends React.HTMLAttributes<HTMLDivElement> {
  label?: string
}

const AppButton = (props: AppButtonProps) => {
  let className = "app-button w-100 text-center" +
    (props.className ? " " + props.className : "");

  if (props.label)
    return <label className={className}>{props.label}{props.children}</label>;

  return <div className={className}>{props.children}</div>;
}

export default AppButton;
