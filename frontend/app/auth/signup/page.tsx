"use client";
import { useForm } from "react-hook-form";
import { useRouter } from "next/navigation";
import { useToast } from "@/hooks/use-toast";
import axios from 'axios';

export default function Signup() {
    const router = useRouter();
    const { toast } = useToast();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm();    

    const formSubmitted = async (data: any) => {
        try {
            const requestData = {
                Username: data.fullname,
                Email: data.email,
                Password: data.password,
            };

            const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/user/create`, requestData);

            if (ssresponse) {
                toast({
                    variant: "success",
                    title: "Registration Successful!",
                    description: "Your account has been created successfully.",
                });

                router.push("/auth/login");
            }
        } catch (error) {
            toast({
                variant: "destructive",
                title: "Registration Failed",
                description: "An error occurred while registering.",
            });
        }
    };

    return (
            <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-3xl p-8 mt-10 mb-10">
                <h2 className="text-2xl font-bold mb-6 text-center text-gray-800">Registration Form</h2>

                <form onSubmit={handleSubmit(formSubmitted)}>
                    <div className="grid grid-cols-1 gap-1">
                        {/* Left Column */}
                        <div>
                            {/* Name */}
                            <div className="mb-4">
                                <label htmlFor="fullname" className="block text-gray-700 font-medium mb-2">Full Name</label>
                                <input {...register("fullname", {
                                    required: "Name is required"
                                })}
                                type="text"
                                placeholder="Type your Name"
                                className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-rose-600"
                                />
                                {errors.fullname && typeof errors.fullname.message === "string" && (
                                    <span className="text-red-700 text-sm font-bold">{errors.fullname.message}</span>
                                )}
                            </div>

                            {/* Email */}
                            <div className="mb-4">  
                                <label htmlFor="email" className="block text-gray-700 font-medium mb-2">Email</label>
                                <input {...register("email", {
                                    required: "Email is required"
                                })}
                                type="email"
                                placeholder="Type your email"
                                className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-rose-600"
                                />
                                {errors.email && typeof errors.email.message === "string" && (
                                    <span className="text-red-700 text-sm font-bold">{errors.email.message}</span>
                                )}
                            </div>

                            {/* Password */}
                            <div className="mb-4">
                                <label htmlFor="password" className="block text-gray-700 font-medium mb-2">Password</label>
                                <input
                                {...register("password", {
                                    required: "Password is required"
                                })}
                                type="password"
                                placeholder="Type your Password"
                                className="w-full px-3 py-2 border rounded-lg"
                                />
                                {errors.password && typeof errors.password.message === "string" && (
                                    <span className="text-red-700 text-sm font-bold">{errors.password.message}</span>
                                )}
                            </div>
                        </div>
                    </div>
                    {/* Submit Button */}
                    <div className="col-span-2 flex justify-center">
                        <button type="submit" className="text-white bg-gradient-to-r from-green-400 via-green-500 to-green-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-green-300 dark:focus:ring-green-800 shadow-lg shadow-green-500/50 dark:shadow-lg dark:shadow-green-800/80 font-medium rounded-lg text-sm px-20 py-2.5 text-center mt-4 cursor-pointer">Submit</button>
                    </div>
                </form>
            </div>
    );
}
