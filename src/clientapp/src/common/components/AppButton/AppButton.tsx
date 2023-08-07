import React from "react";
import "./AppButton.scss";

interface AppButtonProps extends React.HTMLAttributes<HTMLDivElement> {
  label?: string
}

export default function AppButton(props: AppButtonProps) {
  let className = [props.className, "app-button"].join(" ");

  return props.label === null
    ? <div className={className}>{props.children}</div>
    : <label className={className}>{props.label}{props.children}</label>;
};
