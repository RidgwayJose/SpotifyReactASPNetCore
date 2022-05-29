import Index from "../Index/Index"
import { Choices } from "./Styles"

const SidebarChoice = ({title, Icon}) => {
  return (
    <Choices>
      {
        Icon && <Icon/> 
      }
      {
        Icon ?<h4>{title}</h4>: <a href="index"><h5>{title}</h5></a>
      }
    </Choices>
  )
}

export default SidebarChoice