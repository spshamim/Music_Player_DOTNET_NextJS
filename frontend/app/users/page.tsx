"use client"

import axios from "axios";
import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useToast } from "@/hooks/use-toast";
import DLayout from "@/components/DLayout";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
  } from "@/components/ui/table";
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
  
interface user{
    Id : number;
    Username : string;
    Email: string;
}

export default function Test() {
    const { register, formState : {errors}, watch, reset, handleSubmit } = useForm();
    const { toast } = useToast();
    const [userData, setuserData] = useState<user[]>([]);
    const [isCardVisible, setIsCardVisible] = useState(false);
    var toBeSearched = watch("Name"); 

    useEffect(()=>{
        (async ()=>{
            try {
                if(!toBeSearched){
                    const PResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/user/all`);
                    setuserData(PResponse.data);
                }else{
                    const RResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/user/sn/${toBeSearched}`);
                    setuserData(RResponse.data);
                }
            } catch (error) {
                console.error("Error is :: ", error);
                setuserData([]);
            }
        })()
    },[toBeSearched]);

    const addRecentData = async (data: any) => {
        try {
          const requestData = {
            Username: data.uname,
            Email: data.email,
            Password: data.password,
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/user/create`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "User Created!"
            });
    
            reset();
            setIsCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Create!",
          });
        }
      };

      const deleteData = async (id: number) => {
        try {

          const ssresponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/user/delete/${id}`);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "User Deleted!"
            });
    
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Delete!",
          });
        }
      };

    return (
        <DLayout>
        <div className={`max-w-1xl mx-auto bg-white rounded-lg shadow-lg p-6 flex flex-col items-center ${isCardVisible ? "blur-sm" : ""}`}>
            {/* Button to trigger the card visibility */}
            <Button
                className="text-white bg-gradient-to-r from-purple-500 via-purple-600 to-purple-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-purple-300 dark:focus:ring-purple-800 shadow-lg shadow-purple-500/50 dark:shadow-lg dark:shadow-purple-800/80 font-medium rounded-lg text-sm px-10 py-2.5 text-center me-2 mb-2"
                onClick={() => setIsCardVisible(true)}
            >
                Add
            </Button>
            <br/>
            <input 
                type="text"
                placeholder="Type Username"
                {...register('Name')}
                className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md shadow-lg ring-2 ring-indigo-500 focus:ring-4 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            /><br/>

            {/* Viewing the Data */}

            {userData.length === 0 ? (
                    <h1 className="text-1xl text-red-600 font-bold">No data</h1>
                ) : (
                    <Table>
                    <TableCaption>A list of Users.</TableCaption>
                    <TableHeader>
                        <TableRow>
                        <TableHead className="w-[100px] text-left">Id</TableHead>
                        <TableHead className="text-center">User Name</TableHead>
                        <TableHead>User Email</TableHead>
                        <TableHead className="text-right">Action</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {userData.map((data, key) => (
                        <TableRow key={key}>
                            <TableCell className="font-medium text-left">{data.Id}</TableCell>
                            <TableCell className="text-center">{data.Username}</TableCell>
                            <TableCell>{data.Email}</TableCell>
                            <TableCell className="text-right">
                            <button type="button"
                                    className="text-white bg-gradient-to-r from-green-400 via-green-500 to-green-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-green-300 dark:focus:ring-green-800 shadow-lg shadow-green-500/50 dark:shadow-lg dark:shadow-green-800/80 font-medium rounded-lg text-sm px-3 py-1.5 text-center me-2 mb-2"
                                    onClick={()=>{
                                        alert(data.Id);
                                    }}
                                > Update</button>
                            <button type="button"
                                    className="text-white bg-gradient-to-r from-red-400 via-red-500 to-red-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 shadow-lg shadow-red-500/50 dark:shadow-lg dark:shadow-red-800/80 font-medium rounded-lg text-sm px-3 py-1.5 text-center me-2 mb-2"
                                    onClick={()=>{
                                      deleteData(data.Id);
                                    }}
                                > Delete</button>
                            </TableCell>
                        </TableRow>
                        ))}
                    </TableBody>
                    </Table>
                )}

        {/* End of Viewing Data */}
    </div>
    
    {/* Modal Backdrop & Card */}
    {isCardVisible && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50 backdrop-blur-sm">
          <Card className="w-[350px] z-50">
            <CardHeader>
              <CardTitle>Add User</CardTitle>
              <CardDescription>
                Write down the details to add.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form>
                <div className="grid w-full items-center gap-4">
                <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="name">Name</Label>
                    <Input
                        type="text"
                        placeholder="Enter Name"
                        {...register("uname", {
                            required: "Name is required"
                        })}
                    />
                    {errors.uname && typeof errors.uname.message === "string" && (
                        <span className="text-red-700 text-sm font-bold">{errors.uname.message}</span>
                    )}
                </div>
                <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="email">Email</Label>
                    <Input
                        type="email"
                        placeholder="Enter Email"
                        {...register("email", {
                            required: "Email is required"
                        })}
                    />
                    {errors.email && typeof errors.email.message === "string" && (
                        <span className="text-red-700 text-sm font-bold">{errors.email.message}</span>
                    )}
                </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="password">Password</Label>
                    <Input
                      type="password"
                      placeholder="Enter Password"
                      {...register("password", {
                          required: "Password is required",
                      })}
                    />
                    {errors.password && typeof errors.password.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{errors.password.message}</span>)}
                  </div>
                </div>
              </form>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="outline" onClick={() => setIsCardVisible(false)}>
                Cancel
              </Button>
              <Button onClick={handleSubmit(addRecentData)}>Add</Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </DLayout>
    );
}